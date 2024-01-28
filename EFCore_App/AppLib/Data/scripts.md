

```sql
-- Export to Json
SELECT row_to_json(il_data) FROM (SELECT id "IlId", il "IlAdi" FROM public."iller") il_data
SELECT row_to_json(ilce_data) FROM (SELECT id "IlceId", ilce "IlceAdi", ilid "IlId" FROM public."ilceler") ilce_data
SELECT row_to_json(sbb_data) FROM (SELECT id "SemtBucakBeldeId", semt_bucak_belde "SemtBucakBeldeAdi", ilceid "IlceId" FROM public."semtbucakbeldeler") sbb_data
SELECT row_to_json(mahalle_data) FROM (SELECT id "MahalleId", mahalle "MahalleAdi", pk "PK", sbbid "SemtBucakBeldeId" FROM public."mahalleler") mahalle_data
SELECT row_to_json(mahalle_data) FROM (SELECT * FROM public."mahalleler") mahalle_data
```