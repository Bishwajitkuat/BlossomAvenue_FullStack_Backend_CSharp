
-- insert dummy products
INSERT INTO products(product_id,title, description) VALUES('8915e175-4e42-4cbc-b3f2-6a92cda1825b','White Rose', 'Description of white rose.');
INSERT INTO products(product_id ,title, description) VALUES('b6b562e8-b94e-48a3-87bb-5c5497e2a233','Red Rose', 'Description');
INSERT INTO products(product_id ,title, description) VALUES('d780784c-49b3-4442-9adf-f5f8a970333b','Floral Rainbow', 'Description');
INSERT INTO products(product_id,title, description) VALUES('778f52e9-ee57-4c4f-950d-ed4c28007afc','Box of Emotions', 'Description');
INSERT INTO products(product_id,title, description) VALUES('f8a9823c-41a5-4592-a847-59a75fe1a403','Summer Mood', 'Description');
INSERT INTO products(product_id,title, description) VALUES('a9a6cbae-9ad4-4c4e-92d4-8515dda2b857','Romantic Mix', 'Description');


-- insert dummy variations
INSERT INTO variations(variation_name, price, inventory, product_id) VALUES('10 White Rose', 12.50, 10, '8915e175-4e42-4cbc-b3f2-6a92cda1825b');
INSERT INTO variations(variation_name, price, inventory, product_id) VALUES('35 White Rose', 19.99, 5, '8915e175-4e42-4cbc-b3f2-6a92cda1825b');

INSERT INTO variations(variation_name, price, inventory, product_id) VALUES('10 Red Rose', 15.99, 5, 'b6b562e8-b94e-48a3-87bb-5c5497e2a233');

INSERT INTO variations(variation_name, price, inventory, product_id) VALUES('Floral Rainbow normal', 55.99, 5, 'd780784c-49b3-4442-9adf-f5f8a970333b');

INSERT INTO variations(variation_name, price, inventory, product_id) VALUES('Box of Emotions small', 25.99, 4, '778f52e9-ee57-4c4f-950d-ed4c28007afc');

INSERT INTO variations(variation_name, price, inventory, product_id) VALUES('Summer Mood Special', 75.99, 4, 'f8a9823c-41a5-4592-a847-59a75fe1a403');

INSERT INTO variations(variation_name, price, inventory, product_id) VALUES('Romantic Mix Wedding addition', 175.99, 4, 'a9a6cbae-9ad4-4c4e-92d4-8515dda2b857');

-- dummy data insert into images
INSERT INTO images(image_url, product_id) VALUES ('img url1', '8915e175-4e42-4cbc-b3f2-6a92cda1825b');
INSERT INTO images(image_url, product_id) VALUES ('img url2', '8915e175-4e42-4cbc-b3f2-6a92cda1825b');
INSERT INTO images(image_url, product_id) VALUES ('img url', 'b6b562e8-b94e-48a3-87bb-5c5497e2a233');
INSERT INTO images(image_url, product_id) VALUES ('img url', 'd780784c-49b3-4442-9adf-f5f8a970333b');
INSERT INTO images(image_url, product_id) VALUES ('img url', '778f52e9-ee57-4c4f-950d-ed4c28007afc');
INSERT INTO images(image_url, product_id) VALUES ('img url', 'f8a9823c-41a5-4592-a847-59a75fe1a403');
INSERT INTO images(image_url, product_id) VALUES ('img url', 'a9a6cbae-9ad4-4c4e-92d4-8515dda2b857');

-- insert dummy data into categories table
INSERT INTO categories(category_id ,category_name, parent_id) VALUES('4e41661a-94df-4056-bdf0-9d2022a42921','Bouquets',null);
INSERT INTO categories(category_id,category_name, parent_id) VALUES('80ed4a83-807a-4ebc-9097-95ef85a86c3e','Flower type','4e41661a-94df-4056-bdf0-9d2022a42921');
INSERT INTO categories(category_id,category_name, parent_id) VALUES('b5de9995-e20b-4992-bfa9-6cb2bdf71acf','Rose','80ed4a83-807a-4ebc-9097-95ef85a86c3e');

INSERT INTO categories(category_id,category_name, parent_id) VALUES('36597785-b5dc-4d22-a849-ff9ab2e28793','Occasions','4e41661a-94df-4056-bdf0-9d2022a42921');

INSERT INTO categories(category_id,category_name, parent_id) VALUES('55d34b8c-ca70-4c35-b8c9-7865649e95b5','Wedding','36597785-b5dc-4d22-a849-ff9ab2e28793');

INSERT INTO categories(category_id,category_name, parent_id) VALUES('0b79b9f2-2929-422f-b0ba-1ac4f1778ab6','Plants',null);

-- insert into products categories

INSERT INTO products_categories(category_id, product_id) VALUES('55d34b8c-ca70-4c35-b8c9-7865649e95b5', 'a9a6cbae-9ad4-4c4e-92d4-8515dda2b857');
INSERT INTO products_categories(category_id, product_id) VALUES('36597785-b5dc-4d22-a849-ff9ab2e28793','f8a9823c-41a5-4592-a847-59a75fe1a403');
INSERT INTO products_categories(category_id, product_id) VALUES('b5de9995-e20b-4992-bfa9-6cb2bdf71acf','b6b562e8-b94e-48a3-87bb-5c5497e2a233');
INSERT INTO products_categories(category_id, product_id) VALUES('b5de9995-e20b-4992-bfa9-6cb2bdf71acf','8915e175-4e42-4cbc-b3f2-6a92cda1825b');
