SELECT 'CREATE ROLE lab_user LOGIN PASSWORD ''lab_pass'''
WHERE NOT EXISTS (SELECT FROM pg_catalog.pg_roles WHERE rolname = 'lab_user')\gexec

SELECT 'CREATE DATABASE lab_netmicro_payments OWNER lab_user'
WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 'lab_netmicro_payments')\gexec

SELECT 'CREATE DATABASE lab_netmicro_jobs OWNER lab_user'
WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 'lab_netmicro_jobs')\gexec

SELECT 'CREATE DATABASE lab_netmicro_policy OWNER lab_user'
WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 'lab_netmicro_policy')\gexec

SELECT 'CREATE DATABASE lab_netmicro_pricing OWNER lab_user'
WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 'lab_netmicro_pricing')\gexec

GRANT ALL PRIVILEGES ON DATABASE lab_netmicro_payments TO lab_user;
GRANT ALL PRIVILEGES ON DATABASE lab_netmicro_jobs TO lab_user;
GRANT ALL PRIVILEGES ON DATABASE lab_netmicro_policy TO lab_user;
GRANT ALL PRIVILEGES ON DATABASE lab_netmicro_pricing TO lab_user;
