-- Create order from cart --
CREATE OR REPLACE PROCEDURE add_review(p_id UUID, u_id UUID, o_id UUID, r_text TEXT, rating INT)
LANGUAGE plpgsql AS
$$
BEGIN
    -- CHECK IF THE USER HAS MADE THE ORDER --
    IF NOT EXISTS (SELECT * FROM orders WHERE order_id = o_id AND user_id = u_id) THEN RAISE EXCEPTION 'User or Order not found';
	END	IF;
	
	INSERT INTO product_reviews (user_id, product_id, order_id, review, star)
	VALUES (u_id, p_id, o_id, r_text, rating);
END;
$$;


-- Update review --
CREATE OR REPLACE PROCEDURE update_review(r_id UUID, r_text TEXT, rating INT)
LANGUAGE plpgsql AS
$$
BEGIN
    -- CHECK IF THE REVIEW EXISTS --
    IF NOT EXISTS (SELECT * FROM product_reviews WHERE review_id = r_id) THEN RAISE EXCEPTION 'Review not found';
	END	IF;
	
	UPDATE product_reviews SET 
	review = r_text,
	star = rating;
END;
$$;


-- Get single product review by user--
CREATE OR REPLACE FUNCTION get_single_product_review_by_user(p_id UUID, u_id UUID)
RETURNS TABLE (
    review_id UUID,
    product_id UUID,
    review TEXT,
    order_id UUID,
    star INT,
    user_id UUID
)
LANGUAGE plpgsql AS
$$
BEGIN
    RETURN QUERY
    SELECT 
        r.review_id,
        r.product_id,
        r.review,
        r.order_id,
        r.star,
        r.user_id
    FROM 
        product_reviews r
    WHERE 
        r.product_id = p_id AND r.user_id = u_id;
		
	IF NOT FOUND THEN
        RAISE EXCEPTION 'User Or Product not found!';
    END IF;
END;
$$;

-- Get all reviews from single product --
CREATE OR REPLACE FUNCTION get_reviews_by_product(p_id UUID)
RETURNS TABLE (
    review_id UUID,
    product_id UUID,
    review TEXT,
    order_id UUID,
    star INT,
    user_id UUID
)
LANGUAGE plpgsql AS
$$
BEGIN
    RETURN QUERY
    SELECT 
        r.review_id,
        r.product_id,
        r.review,
        r.order_id,
        r.star,
        r.user_id
    FROM 
        product_reviews r
    WHERE 
        r.product_id = p_id;
		
	IF NOT FOUND THEN
        RAISE EXCEPTION 'User not found!';
    END IF;
END;
$$;

-- Get all reviews from single user --
CREATE OR REPLACE FUNCTION get_reviews_by_user(u_id UUID)
RETURNS TABLE (
    review_id UUID,
    product_id UUID,
    review TEXT,
    order_id UUID,
    star INT,
    user_id UUID
)
LANGUAGE plpgsql AS
$$
BEGIN
    RETURN QUERY
    SELECT 
        r.review_id,
        r.product_id,
        r.review,
        r.order_id,
        r.star,
        r.user_id
    FROM 
        product_reviews r
    WHERE 
        r.user_id = u_id;
	IF NOT FOUND THEN
        RAISE EXCEPTION 'User not found!';
    END IF;
END;
$$;