
-- function to get all product with parameters

CREATE OR REPLACE FUNCTION get_all_products_fn(
  page_num INT, 
  item_per_page INT, 
  search_word VARCHAR, 
  sot_by VARCHAR, 
  sot_order VARCHAR)
RETURNS TABLE(
  product_id UUID, 
  title VARCHAR, 
  description TEXT, 
  min_price DECIMAL, 
  image_url TEXT, 
  avg_star DECIMAL)
AS
$$
DECLARE
  query TEXT;
  sort_by VARCHAR := sot_by;
  sort_order VARCHAR := sot_order;
  products_offset INT = page_num*item_per_page - item_per_page;
  products_limit INT = item_per_page;
  allowed_sort_by CONSTANT TEXT[] := ARRAY['title', 'min_price', 'avg_star'];
  allowed_sort_order CONSTANT TEXT[] := ARRAY['ASC', 'DESC'];
BEGIN
      IF page_num IS NULL OR page_num < 1 THEN
      RAISE EXCEPTION 'Page number can not be null or smaller than 1.';
    END IF;

    IF item_per_page IS NULL OR item_per_page < 1 THEN
      RAISE EXCEPTION 'Item per page can not be null or smaller than 1.';
    END IF;
    IF NOT (sot_by = ANY(allowed_sort_by)) THEN
      sort_by := 'title';
    END IF;
    IF NOT (sot_order= ANY(allowed_sort_order)) THEN
      sort_order := 'ASC';
    END IF;

  query := 'WITH sub AS
  (
    SELECT v.product_id, 
    MIN(v.price) AS min_p, 
    MIN(i.image_url) AS image_url, 
    AVG(r.star) AS star 
    FROM variations v
    JOIN images i
    ON v.product_id = i.product_id
    LEFT JOIN product_reviews r
    ON v.product_id = r.product_id
    GROUP BY v.product_id
  )
  SELECT 
  products.product_id, 
  products.title, 
  products.description, 
  s.min_p AS min_price, 
  s.image_url, 
  CAST(s.star AS DECIMAL(10,1)) AS avg_star 
  FROM products
  JOIN sub s
  ON products.product_id = s.product_id
  WHERE products.title ILIKE CONCAT(''%'|| search_word || '%'')
  ORDER BY '||sort_by||' '||sort_order|| '
  LIMIT '|| products_limit|| ' OFFSET ' || products_offset;


RETURN QUERY EXECUTE query;

END;
$$ LANGUAGE PLPGSQL;


-- function to get a products by id

CREATE OR REPLACE FUNCTION get_product_by_id(id UUID)
RETURNS TABLE
  (
  product_id UUID, 
  title VARCHAR, 
  description TEXT, 
  min_price DECIMAL, 
  image_url TEXT
  )
AS
$$
BEGIN
RETURN QUERY
WITH sub AS
  (
    SELECT v.product_id, MIN(v.price) AS min_p, MIN(i.image_url) AS image_url FROM variations v
    JOIN images i
    ON v.product_id = i.product_id
    WHERE v.product_id = id
    GROUP BY v.product_id
  )
SELECT 
  p.product_id, 
  p.title, 
  p.description, 
  s.min_p, 
  s.image_url 
  FROM products p
  JOIN sub s
  ON p.product_id = s.product_id;
END;
$$ LANGUAGE PLPGSQL;



