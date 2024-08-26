-- create order_items table --
CREATE TABLE order_items(
  order_items_id UUID DEFAULT gen_random_uuid() PRIMARY KEY,
  order_id UUID NOT NULL,
  product_id UUID NOT NULL,
  quantity INT,
  price NUMERIC,
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (order_id) 
        REFERENCES orders (order_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT ,
  FOREIGN KEY (product_id) 
        REFERENCES products (product_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
);
