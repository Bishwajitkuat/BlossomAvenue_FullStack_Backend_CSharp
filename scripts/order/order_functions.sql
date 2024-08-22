-- Create order from cart --
CREATE OR REPLACE PROCEDURE create_order_from_cart(p_cart_id UUID, p_user_id UUID, p_total_amount NUMERIC)
LANGUAGE plpgsql AS
$$
DECLARE
    v_order_id UUID;
	total NUMERIC;
BEGIN
    -- Insert a new order into the orders table
    INSERT INTO orders (
        order_id, 
        user_id, 
        order_status, 
        created_at, 
        total_amount, 
        address_id, 
        payment_method
    )
    VALUES (
        gen_random_uuid(),
        p_user_id,
        'pending',
        CURRENT_TIMESTAMP,
        p_total_amount,
        NULL,
        'cash'
    )
    RETURNING order_id INTO v_order_id;

    -- Insert the cart items into the order_items table
    INSERT INTO order_items (
        order_id,
        product_id,
        quantity,
        price
    )
    SELECT 
        v_order_id,
        ci.product_id,
        ci.quantity,
        ci.amount
    FROM 
        cart_items ci
    WHERE 
        ci.cart_id = p_cart_id;

    -- Delete the cart items from the cart_items table
    DELETE FROM cart_items 
    WHERE cart_id = p_cart_id;

    -- Optionally, handle any additional operations or logging
    RAISE NOTICE 'Order % created and cart items deleted.', v_order_id;
	
	call calculate_cart_total(p_cart_id, total);
	UPDATE cart SET total_amount = total;
END;
$$;


-- Get single order details--
CREATE OR REPLACE FUNCTION get_order_details(p_order_id UUID)
RETURNS TABLE (
    order_id UUID,
    user_id UUID,
    order_status order_status,
    created_at TIMESTAMP,
    total_amount NUMERIC,
    address_id UUID,
    payment_method PAYMENT_METHOD_TYPE
)
LANGUAGE plpgsql AS
$$
BEGIN
    -- Return the order details where order_id matches the provided ID
    RETURN QUERY
    SELECT 
        o.order_id,
        o.user_id,
        o.order_status,
        o.created_at,
        o.total_amount,
        o.address_id,
        o.payment_method
    FROM 
        orders o
    WHERE 
        o.order_id = p_order_id;

    -- If no order is found, raise an exception
    IF NOT FOUND THEN
        RAISE EXCEPTION 'Order with ID % does not exist', p_order_id;
    END IF;
END;

$$;


-- Get all orders from user--
CREATE OR REPLACE FUNCTION get_all_orders_by_user(p_user_id UUID)
RETURNS TABLE (
    order_id UUID,
    user_id UUID,
    order_status order_status,
    created_at TIMESTAMP,
    total_amount NUMERIC,
    address_id UUID,
    payment_method PAYMENT_METHOD_TYPE
)
LANGUAGE plpgsql AS
$$
BEGIN
    -- Return all orders for the specified user
    RETURN QUERY
    SELECT 
        o.order_id,
        o.user_id,
        o.order_status,
        o.created_at,
        o.total_amount,
        o.address_id,
        o.payment_method
    FROM 
        orders o
    WHERE 
        o.user_id = p_user_id;
END;
$$;
