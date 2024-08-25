-- Calculate cart total --
CREATE OR REPLACE PROCEDURE calculate_cart_total(c_id UUID, OUT total_amount NUMERIC)
LANGUAGE plpgsql AS
$$
BEGIN
    -- Initialize the total_amount to 0
    total_amount := 0;

    -- Calculate the total amount for all items in the cart
    SELECT SUM(quantity * amount)
    INTO total_amount
    FROM cart_items
    WHERE cart_id = c_id;

    -- If no items were found for the given cart_id, set total_amount to 0
    IF total_amount IS NULL THEN
        total_amount := 0;
    END IF;
END;
$$;

-- Add to cart--
CREATE OR REPLACE PROCEDURE add_to_cart(c_id uuid, p_id uuid, qty int, amnt numeric)
LANGUAGE plpgsql AS 
$$
DECLARE
	cart_total NUMERIC;
BEGIN
	-- begin; unnecessary
	-- if user_id exist?	
	INSERT INTO cart_items (cart_id, product_id, quantity, amount)
	VALUES (c_id, p_id, qty, amnt);
	
	-- calculate total amount --
	call calculate_cart_total(c_id, cart_total);
	UPDATE cart SET total_amlount = cart_total;
END;
$$;

--Update cart--
CREATE OR REPLACE PROCEDURE update_cart_item_quantity(p_cart_id UUID, p_product_id UUID, p_quantity INT)
LANGUAGE plpgsql AS
$$
DECLARE total NUMERIC;
BEGIN
    -- Update the quantity for the specified cart and product
    UPDATE cart_items 
    SET quantity = p_quantity
    WHERE cart_id = p_cart_id 
      AND product_id = p_product_id;

    -- Check if the update affected any rows
    IF NOT FOUND THEN
        RAISE EXCEPTION 'No item found for cart ID % and product ID %', p_cart_id, p_product_id;
    END IF;
	call calculate_cart_total(p_cart_id, total);
	UPDATE cart SET total_amount = total;
END;
$$;

--Delete item from cart--
CREATE OR REPLACE PROCEDURE remove_product_from_cart(p_cart_id UUID, p_product_id UUID)
LANGUAGE plpgsql AS
$$
DECLARE total NUMERIC;
BEGIN
    -- Delete the cart item where both cart_id and product_id match
    DELETE FROM cart_items 
    WHERE cart_id = p_cart_id 
      AND product_id = p_product_id;

    -- Check if the delete operation affected any rows
    IF NOT FOUND THEN
        RAISE EXCEPTION 'No item found for cart ID % and product ID %', p_cart_id, p_product_id;
    END IF;
	
	call calculate_cart_total(p_cart_id, total);
	UPDATE cart SET total_amount = total;
END;
$$;
