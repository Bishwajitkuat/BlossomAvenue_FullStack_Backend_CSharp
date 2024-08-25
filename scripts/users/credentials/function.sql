-- Create Login Function
CREATE OR REPLACE FUNCTION create_user_credentials(
    p_user_id UUID, 
    p_username VARCHAR(50),
    p_password TEXT
)
RETURNS VOID AS $$
BEGIN

    -- Validation
    IF p_user_id IS NULL THEN
        RAISE EXCEPTION 'Error: user_id cannot be null';
    END IF;
    IF p_username IS NULL OR p_username = '' THEN
        RAISE EXCEPTION 'Error: username cannot be null or empty';
    END IF;
    IF p_password IS NULL OR p_password = '' THEN
        RAISE EXCEPTION 'Error: password cannot be null or empty';
    END IF;

	--Check if any password record exist from the user
	IF EXISTS (SELECT 1 FROM user_credentials WHERE user_credentials.user_id = p_user_id) THEN
        RAISE EXCEPTION 'Error: A password already beign assigned';
    END IF;

    -- Insert password --
    INSERT INTO user_credentials (user_id, user_name, password)
    VALUES (p_user_id, p_username, p_password);
    
END;
$$ LANGUAGE plpgsql;

-- Update Credentials --

CREATE OR REPLACE FUNCTION update_user_credentials(
    p_user_id UUID, 
    p_password TEXT
)
RETURNS BOOLEAN AS $$
DECLARE 
	rows_affected INT;
BEGIN

    -- Validation
    IF p_user_id IS NULL THEN
        RAISE EXCEPTION 'Error: user_id cannot be null';
    END IF;
    IF p_password IS NULL OR p_password = '' THEN
        RAISE EXCEPTION 'Error: password cannot be null or empty';
    END IF;

	--Check if any password record exist from the user
	IF NOT EXISTS (SELECT 1 FROM user_credentials WHERE user_credentials.user_id = p_user_id) THEN
        RAISE EXCEPTION 'Error: No credentials found from the provided id';
    END IF;

    -- Update password --
	UPDATE user_credentials 
	SET password = p_password 
	WHERE user_id = p_user_id;

	 -- Check how many rows were affected by the delete operation
    GET DIAGNOSTICS rows_affected = ROW_COUNT;

    RETURN rows_affected > 0;
    
END;
$$ LANGUAGE plpgsql;