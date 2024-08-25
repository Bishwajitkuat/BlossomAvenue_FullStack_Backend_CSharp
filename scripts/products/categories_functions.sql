-- function to get all categories
CREATE OR REPLACE FUNCTION get_all_categories() 
RETURNS TABLE(category_id UUID, category_name VARCHAR, parent_id UUID)
AS 
$$
  BEGIN

  RETURN QUERY
  SELECT c.category_id, c.category_name, c.parent_id FROM categories c;

END;
$$ LANGUAGE PLPGSQL;

-- function to get category by id
CREATE OR REPLACE FUNCTION get_category_by_id(id UUID)
RETURNS TABLE(category_id UUID, category_name VARCHAR, parent_id UUID)
AS
$$
BEGIN
    IF id IS NULL OR NOT EXISTS (SELECT c.category_id FROM categories c) THEN
      RAISE EXCEPTION 'Category id can not be null.';
    END IF;
  RETURN QUERY
  SELECT c.category_id, c.category_name, c.parent_id FROM categories c WHERE c.category_id = id;
END;
$$ LANGUAGE PLPGSQL;


-- function to update a category by id
CREATE OR REPLACE FUNCTION update_category_by_id(cat_id UUID, cat_name VARCHAR, par_id UUID)
RETURNS TABLE(cate_id UUID, cate_name VARCHAR, pare_id UUID)
AS
$$
BEGIN
  IF NOT EXISTS (SELECT category_id FROM categories WHERE category_id=cat_id) THEN RAISE EXCEPTION 'Category does not exist.';
  END IF;
  IF NOT EXISTS (SELECT parent_id FROM categories WHERE parent_id=par_id) THEN RAISE EXCEPTION 'Parent id does not exist.';
  END IF;
  UPDATE categories SET category_name=cat_name, parent_id=par_id WHERE category_id=cat_id;
  RETURN QUERY
  SELECT * FROM categories cr WHERE cr.category_id = cat_id; 

END;
$$ LANGUAGE PLPGSQL;


-- function to create new category
CREATE OR REPLACE FUNCTION create_category(cat_name VARCHAR, par_id UUID)
RETURNS TABLE(cate_id UUID, cate_name VARCHAR, pare_id UUID)
AS
$$
DECLARE
  new_category_id UUID;
BEGIN
  IF (par_id IS NOT NULL AND NOT EXISTS (SELECT c.parent_id FROM categories c WHERE c.parent_id=par_id) ) THEN RAISE EXCEPTION 'Parent id does not exist.';
  END IF;
  INSERT INTO categories(category_name, parent_id) VALUES(cat_name, par_id) RETURNING category_id INTO new_category_id;
  RETURN QUERY
  SELECT * FROM categories c WHERE c.category_id = new_category_id;
END;
$$ LANGUAGE PLPGSQL;



-- Function to delete a category
CREATE OR REPLACE FUNCTION delete_category_by_id(id UUID)
RETURNS BOOLEAN
AS
$$
BEGIN
  IF NOT EXISTS (SELECT category_id FROM categories WHERE category_id = id)
  THEN RAISE EXCEPTION 'Category can not be found';
  END IF;
  IF EXISTS (SELECT c.category_id FROM categories c WHERE c.parent_id = id) THEN
    RAISE EXCEPTION 'This category can not be deleted as it is used as a reference   for parent category in other category.';
  END IF;
  DELETE FROM categories WHERE category_id=id;
  RETURN TRUE;
END;
$$ LANGUAGE PLPGSQL;