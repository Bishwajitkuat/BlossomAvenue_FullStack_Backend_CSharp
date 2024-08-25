-- create orders table --
CREATE TABLE product_reviews(
  review_id UUID DEFAULT gen_random_uuid() PRIMARY KEY,
  user_id UUID NOT NULL,
  product_id UUID NOT NULL,
  order_id UUID NOT NULL,
  review TEXT,
  star INT, 
  CHECK (star <= 5),
  FOREIGN KEY (user_id) 
        REFERENCES users (user_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT ,
  FOREIGN KEY (product_id) 
        REFERENCES products (product_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,
  FOREIGN KEY (order_id) 
      REFERENCES orders (order_id)
      ON UPDATE CASCADE
      ON DELETE RESTRICT
);
