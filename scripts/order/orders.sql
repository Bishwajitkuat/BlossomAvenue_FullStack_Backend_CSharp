--create order status type --
CREATE TYPE public.order_status AS ENUM
    ('pending', 'paid', 'cancelled');

ALTER TYPE public.order_status
    OWNER TO postgres;
	
--create payment method type --
CREATE TYPE public.payment_method_type AS ENUM
    ('card', 'cash');

ALTER TYPE public.payment_method_type
    OWNER TO postgres;

-- create orders table --
CREATE TABLE orders(
  order_id UUID DEFAULT gen_random_uuid() PRIMARY KEY,
  user_id UUID NOT NULL,
  order_status order_status,
  address_id UUID,
  total_amount INT,
  payment_method payment_method_type,
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (user_id) 
        REFERENCES users (user_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT ,
  FOREIGN KEY (address_id) 
        REFERENCES address_details (address_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
);
