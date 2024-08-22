-- create cart_items table --
CREATE TABLE cart_items(
  cart_items_id UUID DEFAULT gen_random_uuid() PRIMARY KEY,
  cart_id UUID NOT NULL,
  product_id UUID NOT NULL,
  quantity INT,
  amount NUMERIC,
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (cart_id) 
        REFERENCES cart (cart_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT ,
  FOREIGN KEY (product_id) 
        REFERENCES products (product_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
);
