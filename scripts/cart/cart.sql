-- create cart table --
CREATE TABLE cart(
  cart_id UUID DEFAULT gen_random_uuid() PRIMARY KEY,
  user_id UUID NOT NULL,
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  total_amount INT,  
  FOREIGN KEY (user_id) 
	REFERENCES users (user_id)
	ON UPDATE CASCADE
	ON DELETE RESTRICT
);
