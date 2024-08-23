
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
CREATE OR REPLACE FUNCTION get_users()
RETURNS TABLE 
(
user_id UUID, 
first_name VARCHAR, 
last_name VARCHAR, 
email VARCHAR, 
user_role_id UUID,
user_role_name VARCHAR,
last_login TIMESTAMP,
is_user_active BOOLEAN,
created_at TIMESTAMP
) AS $$
BEGIN
	RETURN QUERY
	SELECT 
		users.user_id, 
		users.first_name, 
		users.last_name, 
		users.email, 
		users.user_role_id,
		user_roles.user_role_name,
		users.last_login,
		users.is_user_active,
		users.created_at
	FROM users INNER JOIN
	user_roles
	ON users.user_role_id = user_roles.user_role_id;
END;
$$ LANGUAGE plpgsql;

-- Get one user by id --
CREATE OR REPLACE FUNCTION get_user(p_user_id UUID)
RETURNS TABLE 
(
user_id UUID, 
first_name VARCHAR, 
last_name VARCHAR, 
email VARCHAR, 
user_role_id UUID,
user_role_name VARCHAR,
last_login TIMESTAMP,
is_user_active BOOLEAN,
created_at TIMESTAMP
) AS $$
BEGIN
	RETURN QUERY
	SELECT 
		users.user_id, 
		users.first_name, 
		users.last_name, 
		users.email, 
		users.user_role_id,
		user_roles.user_role_name,
		users.last_login,
		users.is_user_active,
		users.created_at
	FROM users INNER JOIN
	user_roles
	ON users.user_role_id = user_roles.user_role_id
	WHERE users.user_id = p_user_id;
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

-- Delete one user --

CREATE OR REPLACE FUNCTION delete_user(
	p_user_id UUID
)
RETURNS BOOLEAN AS $$
DECLARE
    rows_affected INT;
BEGIN

	-- Check if a user exists with user_id
	IF NOT EXISTS (SELECT 1 FROM users WHERE users.user_id = p_user_id) THEN
        RAISE EXCEPTION 'Error: A user with given user id does not exists';
    END IF;

	DELETE FROM users 
	WHERE 
	   users.user_id = p_user_id;

	GET DIAGNOSTICS rows_affected = ROW_COUNT;

    RETURN rows_affected > 0;
    
END;
$$ LANGUAGE plpgsql;