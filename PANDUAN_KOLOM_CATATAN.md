
ALTER TABLE monitoring 
ADD COLUMN IF NOT EXISTS catatan TEXT DEFAULT '';



'''Setelah menjalankan perintah ALTER TABLE, jalankan perintah ini untuk verifikasi:'''

SELECT column_name, data_type, column_default 
FROM information_schema.columns 
WHERE table_name = 'monitoring' 
ORDER BY ordinal_position;
