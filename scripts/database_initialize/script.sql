
-- create database --
CREATE DATABASE blossom_avenue_db;

-- creating minimum privileged user for the application -- 
CREATE USER app_user WITH PASSWORD {password}; -- replace {password} with a strong password

-- grant app_user to the database --
GRANT CONNECT ON DATABASE blossom_avenue_db TO app_user;

-- grant CRUD permissions on tables --
GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA public TO app_user;

-- ensure future tables and functions also grant the necessary privileges --
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO app_user;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT EXECUTE ON FUNCTIONS TO app_user;

-- ensure user cannot create additional roles, databases, or perform actions beyond the allowed scope --
ALTER USER app_user WITH NOSUPERUSER NOCREATEDB NOCREATEROLE NOREPLICATION;

-- ensure that the pgcrypto extension is enabled for uuid
CREATE EXTENSION IF NOT EXISTS pgcrypto;
