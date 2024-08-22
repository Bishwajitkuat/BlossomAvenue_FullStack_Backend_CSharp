
-- create products table
CREATE TABLE products(
 product_id UUID DEFAULT gen_random_uuid() PRIMARY KEY,
 title VARCHAR(100) NOT NULL,
 description TEXT
);

-- create variations table
CREATE TABLE variations(
variation_id UUID DEFAULT gen_random_uuid() PRIMARY KEY,
variation_name VARCHAR(100) NOT NULL,
price DECIMAL,
inventory INT,
product_id UUID NOT NULL,
FOREIGN KEY (product_id)
        REFERENCES products(product_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
);

-- create images table
CREATE TABLE images(
  image_id UUID DEFAULT gen_random_uuid() PRIMARY KEY,
  image_url TEXT,
  product_id UUID NOT NULL,
  FOREIGN KEY (product_id)
          REFERENCES products(product_id)
          ON UPDATE CASCADE
          ON DELETE RESTRICT
);

-- create categories table
CREATE TABLE categories(
  category_id UUID DEFAULT gen_random_uuid() PRIMARY KEY,
  category_name VARCHAR(100) NOT NULL,
  parent_id UUID REFERENCES categories(category_id)
);

-- create products_categories table
CREATE TABLE products_categories(
  product_category_id UUID DEFAULT gen_random_uuid() PRIMARY KEY,
  category_id UUID NOT NULL,
  product_id UUID NOT NULL,
  FOREIGN KEY (category_id)
          REFERENCES categories(category_id)
          ON UPDATE CASCADE
          ON DELETE RESTRICT,
  FOREIGN KEY (product_id)
          REFERENCES products(product_id)
          ON UPDATE CASCADE
          ON DELETE RESTRICT
);