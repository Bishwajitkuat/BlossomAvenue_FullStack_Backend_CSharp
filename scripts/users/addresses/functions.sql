-- Create address --

CREATE OR REPLACE FUNCTION create_address(
    p_user_id UUID, 
    p_address_line_1 VARCHAR(100),
    p_address_line_2 VARCHAR(100),
    p_city_id UUID
)
RETURNS TABLE (
    address_id UUID, 
    address_line_1 VARCHAR, 
    address_line_2 VARCHAR, 
    city_id UUID
) AS $$
DECLARE
    new_address_id UUID;
BEGIN
    -- Validation
    IF p_user_id IS NULL THEN
        RAISE EXCEPTION 'Error: User id cannot be null or empty';
    END IF;
    IF p_address_line_1 IS NULL OR p_address_line_1 = '' THEN
        RAISE EXCEPTION 'Error: Address line 1 cannot be null or empty';
    END IF;
    IF p_city_id IS NULL THEN
        RAISE EXCEPTION 'Error: City id cannot be null or empty';
    END IF;

    -- Check if user exists
    IF NOT EXISTS (SELECT 1 FROM users WHERE users.user_id = p_user_id) THEN
        RAISE EXCEPTION 'Error: Invalid user id found';
    END IF;

    -- Check if city exists
    IF NOT EXISTS (SELECT 1 FROM cities WHERE cities.city_id = p_city_id) THEN
        RAISE EXCEPTION 'Error: Invalid city id found';
    END IF;

    -- Insert into address_details and return the new address_id
    INSERT INTO address_details (address_line_1, address_line_2, city_id)
    VALUES (p_address_line_1, p_address_line_2, p_city_id)
    RETURNING address_details.address_id INTO new_address_id;

    -- Insert into user_addresses
    INSERT INTO user_addresses (user_id, address_id) 
    VALUES (p_user_id, new_address_id);

    -- Return the newly created address details
    RETURN QUERY 
    SELECT new_address_id, p_address_line_1, p_address_line_2, p_city_id;

END;
$$ LANGUAGE plpgsql;

-- Update address --

CREATE OR REPLACE FUNCTION update_address(
	p_address_id UUID,
    p_address_line_1 VARCHAR(100),
    p_address_line_2 VARCHAR(100),
    p_city_id UUID
)
RETURNS BOOLEAN AS $$
DECLARE
    rows_affected INT;
BEGIN
    -- Validation
    IF p_address_id IS NULL THEN
        RAISE EXCEPTION 'Error: Address id cannot be null or empty';
    END IF;
    IF p_address_line_1 IS NULL OR p_address_line_1 = '' THEN
        RAISE EXCEPTION 'Error: Address line 1 cannot be null or empty';
    END IF;
    IF p_city_id IS NULL THEN
        RAISE EXCEPTION 'Error: City id cannot be null or empty';
    END IF;

    -- Check if address exists
    IF NOT EXISTS (SELECT 1 FROM address_details WHERE address_details.address_id = p_address_id) THEN
        RAISE EXCEPTION 'Error: Invalid address id found';
    END IF;

    -- Check if city exists
    IF NOT EXISTS (SELECT 1 FROM cities WHERE cities.city_id = p_city_id) THEN
        RAISE EXCEPTION 'Error: Invalid city id found';
    END IF;

    UPDATE address_details SET
	address_line_1 = p_address_line_1,
	address_line_2 = p_address_line_2,
	city_id = p_city_id;

	GET DIAGNOSTICS rows_affected = ROW_COUNT;

    RETURN rows_affected > 0;

END;
$$ LANGUAGE plpgsql;

-- Delete address --

CREATE OR REPLACE FUNCTION delete_address(
	p_address_id UUID
)
RETURNS BOOLEAN AS $$
DECLARE
    rows_affected INT;
BEGIN
    -- Validation
    IF p_address_id IS NULL THEN
        RAISE EXCEPTION 'Error: Address id cannot be null or empty';
    END IF;

    -- Check if address exists
    IF NOT EXISTS (SELECT 1 FROM address_details WHERE address_details.address_id = p_address_id) THEN
        RAISE EXCEPTION 'Error: Invalid address id found';
    END IF;

	-- Check if default address
    IF EXISTS (SELECT 1 FROM user_addresses WHERE user_addresses.address_id = p_address_id AND default_address = TRUE) THEN
        RAISE EXCEPTION 'Error: Cannot delete default user address';
    END IF;

	DELETE FROM user_addresses
	WHERE user_addresses.address_id = p_address_id;

    DELETE FROM address_details 
	WHERE address_details.address_id = p_address_id;

	GET DIAGNOSTICS rows_affected = ROW_COUNT;

    RETURN rows_affected > 0;

END;
$$ LANGUAGE plpgsql;



