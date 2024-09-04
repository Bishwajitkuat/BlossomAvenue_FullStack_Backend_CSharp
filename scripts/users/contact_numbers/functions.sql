CREATE OR REPLACE FUNCTION create_contact_number(
    p_user_id UUID,
    p_contact_number VARCHAR[]
)
RETURNS VOID AS $$
BEGIN
    -- Validation
    IF p_user_id IS NULL THEN
        RAISE EXCEPTION 'Error: User id cannot be null';
    END IF;

    -- Check if user exists
    IF NOT EXISTS (SELECT 1 FROM users WHERE users.user_id = p_user_id) THEN
        RAISE EXCEPTION 'Error: Invalid user id found';
    END IF;

    -- Validate contact number array
    IF p_contact_number IS NULL OR array_length(p_contact_number, 1) = 0 THEN
        RAISE EXCEPTION 'Error: The array cannot be null or empty';
    END IF;

    -- Insert each contact number into the user_contact_number table
    INSERT INTO user_contact_number (user_id, contact_number)
    SELECT p_user_id, unnest(p_contact_number);

END;
$$ LANGUAGE plpgsql;

-- Update contact number

CREATE OR REPLACE FUNCTION update_contact_number(
    p_user_id UUID,
    p_contact_number VARCHAR[]
)
RETURNS VOID AS $$
BEGIN
    -- Validation
    IF p_user_id IS NULL THEN
        RAISE EXCEPTION 'Error: User id cannot be null';
    END IF;

    -- Check if user exists
    IF NOT EXISTS (SELECT 1 FROM users WHERE users.user_id = p_user_id) THEN
        RAISE EXCEPTION 'Error: Invalid user id found';
    END IF;

    -- Validate contact number array
    IF p_contact_number IS NULL OR array_length(p_contact_number, 1) = 0 THEN
        RAISE EXCEPTION 'Error: The array cannot be null or empty';
    END IF;

	DELETE FROM user_contact_number WHERE user_contact_number.user_id = p_user_id;

    -- Insert each contact number into the user_contact_number table
    INSERT INTO user_contact_number (user_id, contact_number)
    SELECT p_user_id, unnest(p_contact_number);

END;
$$ LANGUAGE plpgsql;

-- Delete contact number --

CREATE OR REPLACE FUNCTION delete_contact_number(
    p_user_id UUID,
    p_contact_number VARCHAR
)
RETURNS BOOLEAN AS $$
DECLARE
    existing_number_count INT;
    rows_affected INT;
BEGIN
    -- Validation
    IF p_user_id IS NULL THEN
        RAISE EXCEPTION 'Error: User id cannot be null';
    END IF;

    IF p_contact_number IS NULL OR p_contact_number = '' THEN
        RAISE EXCEPTION 'Error: Contact number cannot be null or empty';
    END IF;

    -- Check if user contact exists
    IF NOT EXISTS (
        SELECT 1 
        FROM user_contact_number
        WHERE user_id = p_user_id AND contact_number = p_contact_number
    ) THEN
        RAISE EXCEPTION 'Error: Invalid user or contact number found';
    END IF;

    -- Check if there are other contact numbers for the user
    SELECT COUNT(*) INTO existing_number_count
    FROM user_contact_number
    WHERE user_id = p_user_id AND contact_number <> p_contact_number;

    IF existing_number_count = 0 THEN
        RAISE EXCEPTION 'Error: Cannot delete contact number. At least one contact number should exist.';
    END IF;

    -- Delete the specified contact number
    DELETE FROM user_contact_number
    WHERE user_id = p_user_id AND contact_number = p_contact_number;

    -- Check how many rows were affected by the delete operation
    GET DIAGNOSTICS rows_affected = ROW_COUNT;

    RETURN rows_affected > 0;

END;
$$ LANGUAGE plpgsql;
