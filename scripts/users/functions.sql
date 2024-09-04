
-- Create one user --
CREATE OR REPLACE FUNCTION create_user(
    p_first_name VARCHAR(50), 
    p_last_name VARCHAR(50),
    p_email VARCHAR(100),
    p_user_role_id UUID
)
RETURNS TABLE (user_id UUID, first_name VARCHAR, last_name VARCHAR, email VARCHAR, user_role_id UUID) AS $$
BEGIN

    -- Validation
    IF p_first_name IS NULL OR p_first_name = '' THEN
        RAISE EXCEPTION 'Error: first_name cannot be null or empty';
    END IF;
    IF p_last_name IS NULL OR p_last_name = '' THEN
        RAISE EXCEPTION 'Error: last_name cannot be null or empty';
    END IF;
    IF p_email IS NULL OR p_email = '' THEN
        RAISE EXCEPTION 'Error: email cannot be null or empty';
    END IF;
    IF p_user_role_id IS NULL THEN
        RAISE EXCEPTION 'Error: user_role_id cannot be null or empty';
    END IF;
    
    -- Check if email already exists
    IF EXISTS (SELECT 1 FROM users WHERE users.email = p_email) THEN
        RAISE EXCEPTION 'Error: email has already been used';
    END IF;

    -- Insert new user and return the new row
    RETURN QUERY
    INSERT INTO users (first_name, last_name, email, user_role_id)
    VALUES (p_first_name, p_last_name, p_email, p_user_role_id)
    RETURNING users.user_id, users.first_name, users.last_name, users.email, users.user_role_id;
    
END;
$$ LANGUAGE plpgsql;

-- Get all users -- 
CREATE OR REPLACE FUNCTION get_users(
    p_page_no INT,
    p_page_size INT,
    p_user_role_id UUID,
    p_order_with VARCHAR,
    p_order_by VARCHAR,
    p_search VARCHAR
)
RETURNS TABLE 
(
    user_id UUID, 
    first_name VARCHAR, 
    last_name VARCHAR, 
    email VARCHAR, 
    user_role_id UUID,
    user_role_name VARCHAR
) AS $$
DECLARE
    query TEXT;
    allowed_columns CONSTANT TEXT[] := ARRAY['first_name', 'last_name', 'email', 'user_role_name', 'last_login', 'is_user_active', 'created_at'];
    allowed_orders CONSTANT TEXT[] := ARRAY['ASC', 'DESC'];
BEGIN
    -- Validate inputs
    IF p_page_no IS NULL OR p_page_no < 1 THEN
        RAISE EXCEPTION 'Error: page_no cannot be null or less than 1';
    END IF;
    
    IF p_page_size IS NULL OR p_page_size < 1 THEN
        RAISE EXCEPTION 'Error: page_size cannot be null or less than 1';
    END IF;

    -- Default ordering values
    IF p_order_with IS NULL OR p_order_with = '' THEN
        p_order_with := 'last_name';
    END IF;
    
    IF p_order_by IS NULL OR p_order_by = '' THEN
        p_order_by := 'ASC';
    END IF;
    
    IF p_search IS NULL THEN
        p_search := '';
    END IF;
    
    -- Ensure p_order_with is in the whitelist
    IF NOT (p_order_with = ANY (allowed_columns)) THEN
        RAISE EXCEPTION 'Invalid order_with column name: %', p_order_with;
    END IF;

    -- Ensure p_order_by is in the whitelist
    IF NOT (p_order_by = ANY (allowed_orders)) THEN
        RAISE EXCEPTION 'Invalid order_by value: %', p_order_by;
    END IF;

    -- Build the dynamic query
    query := 'SELECT 
                users.user_id, 
                users.first_name, 
                users.last_name, 
                users.email, 
                users.user_role_id,
                user_roles.user_role_name
              FROM users 
              INNER JOIN user_roles ON users.user_role_id = user_roles.user_role_id
              WHERE (
                users.first_name ILIKE ''%' || p_search || '%'' OR 
                users.last_name ILIKE ''%' || p_search || '%'' OR 
                users.email ILIKE ''%' || p_search || '%''
              )  
              AND (users.user_role_id = $1 OR $1 IS NULL)
              ORDER BY ' || quote_ident(p_order_with) || ' ' || p_order_by || '
              LIMIT ' || p_page_size || ' OFFSET ' || (p_page_no - 1) * p_page_size;

    -- Print query for debugging
    RAISE NOTICE 'Executing query: %', query;

    -- Execute the dynamic query safely
    RETURN QUERY EXECUTE query USING p_user_role_id;

END;
$$ LANGUAGE plpgsql;

-- Update user --
CREATE OR REPLACE FUNCTION update_user(
	p_user_id UUID,
    p_first_name VARCHAR(50), 
    p_last_name VARCHAR(50),
    p_email VARCHAR(100),
    p_is_active BOOLEAN
)
RETURNS BOOLEAN AS $$
DECLARE
    rows_affected INT;
BEGIN

    -- Validation
    IF p_first_name IS NULL OR p_first_name = '' THEN
        RAISE EXCEPTION 'Error: first_name cannot be null or empty';
    END IF;
    IF p_last_name IS NULL OR p_last_name = '' THEN
        RAISE EXCEPTION 'Error: last_name cannot be null or empty';
    END IF;
    IF p_email IS NULL OR p_email = '' THEN
        RAISE EXCEPTION 'Error: email cannot be null or empty';
    END IF;

	-- Check if a user exists with user_id
	IF NOT EXISTS (SELECT 1 FROM users WHERE users.user_id = p_user_id) THEN
        RAISE EXCEPTION 'Error: A user with given user id does not exists';
    END IF;
	
    
    -- Check if email already exists
    IF EXISTS (SELECT 1 FROM users WHERE users.email = p_email AND users.user_id != p_user_id) THEN
        RAISE EXCEPTION 'Error: email has already been used';
    END IF;

	UPDATE users SET 
		first_name = p_first_name, 
		last_name = p_last_name, 
		email = p_email, 
		is_user_active = p_is_active
	WHERE 
	   users.user_id = p_user_id;

	GET DIAGNOSTICS rows_affected = ROW_COUNT;

    RETURN rows_affected > 0;
    
END;
$$ LANGUAGE plpgsql;

-- Get user by Id -- 

CREATE OR REPLACE FUNCTION get_user_by_id(
    p_user_id UUID
)
RETURNS TABLE 
(
    user_id UUID, 
    first_name VARCHAR, 
    last_name VARCHAR, 
    email VARCHAR, 
    user_role_id UUID,
	last_login TIMESTAMP,
	is_user_active BOOLEAN,
	created_at TIMESTAMP,
	address_line_1 VARCHAR,
	address_line_2 VARCHAR,
	city_id UUID,
	contact_number VARCHAR
) AS $$
BEGIN
    -- Validate inputs
    IF p_user_id IS NULL THEN
        RAISE EXCEPTION 'Error: user id cannot be null';
    END IF;

	-- Check if a user exists with user_id
	IF NOT EXISTS (SELECT 1 FROM users WHERE users.user_id = p_user_id) THEN
        RAISE EXCEPTION 'Error: A user with given user id does not exists';
    END IF;

	RETURN QUERY
	SELECT 
      users.user_id,
	  users.first_name,
	  users.last_name,
	  users.email,
	  users.user_role_id,
	  users.last_login,
	  users.is_user_active,
	  users.created_at,
	  address_details.address_line_1,
	  address_details.address_line_2,
	  address_details.city_id,
	  user_contact_number.contact_number,
	  user_addresses.default_address
	FROM users 
	INNER JOIN user_addresses ON user_addresses.user_id = users.user_id
	INNER JOIN address_details ON address_details.address_id = user_addresses.address_id
	INNER JOIN user_contact_number ON user_contact_number.user_id = users.user_id
	WHERE users.user_id = p_user_id;

END;
$$ LANGUAGE plpgsql;