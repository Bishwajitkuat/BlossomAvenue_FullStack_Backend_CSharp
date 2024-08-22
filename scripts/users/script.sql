-- create user_roles table --
CREATE TABLE user_roles(
 user_role_id UUID DEFAULT gen_random_uuid() PRIMARY KEY,
 user_role_name VARCHAR(20) NOT NULL
)

-- create cities table --
CREATE TABLE cities(
  city_id UUID DEFAULT gen_random_uuid() PRIMARY KEY,
  city_name VARCHAR(50) NOT NULL
)

-- create users table --
CREATE TABLE users(
  user_id UUID DEFAULT gen_random_uuid() PRIMARY KEY,
  first_name VARCHAR(50) NOT NULL,
  last_name VARCHAR(50) NOT NULL,
  email VARCHAR(100),
  user_role_id UUID NOT NULL,
  last_login TIMESTAMP,
  is_user_active BOOLEAN DEFAULT TRUE,
  created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
  FOREIGN KEY (user_role_id) 
        REFERENCES user_roles (user_role_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
)

-- create address table --
CREATE TABLE address_details(
  address_id UUID DEFAULT gen_random_uuid() PRIMARY KEY,
  address_line_1 VARCHAR(100) NOT NULL,
  address_line_2 VARCHAR(100),
  city_id UUID,
  FOREIGN KEY (city_id) 
        REFERENCES cities (city_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
)
-- create address_details --
CREATE TABLE user_addresses(
	user_id UUID,
	address_id UUID,
	PRIMARY KEY (user_id, address_id),
	FOREIGN KEY (user_id) 
        REFERENCES users (user_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,
	FOREIGN KEY (address_id) 
        REFERENCES address_details (address_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
)

-- create user contact_number --
CREATE TABLE user_contact_number(
	user_id UUID NOT NULL,
	contact_number VARCHAR(10) NOT NULL,
	FOREIGN KEY (user_id) 
        REFERENCES users (user_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
)

-- create user credentials table 
CREATE TABLE user_credentials(
	user_id UUID NOT NULL,
	user_name VARCHAR(50) NOT NULL,
	password TEXT NOT NULL,
	FOREIGN KEY (user_id) 
        REFERENCES users (user_id)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
)