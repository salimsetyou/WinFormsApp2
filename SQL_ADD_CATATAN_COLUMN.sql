
ALTER TABLE monitoring ADD COLUMN IF NOT EXISTS catatan TEXT DEFAULT '';


SELECT column_name, data_type FROM information_schema.columns 
WHERE table_name = 'monitoring' ORDER BY ordinal_position;
