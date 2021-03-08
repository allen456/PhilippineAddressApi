# API for Philippine Regions, Province/Districts, Cities/Municipalities and Barangays

API to display all Philippine Regions, Province/Districts, Cities/Municipalities and Barangays 
built using .net core 3.1 web-api with entity framework and swagger

### Source 
[PSGC Publication September 2020](https://psa.gov.ph/sites/default/files/attachments/ird/specialrelease/PSGC%203Q%202020%20Publication.xlsx)

### Initial Seed CSV Data
[PSGC_3Q_2020_Publication.csv](./PSGC_3Q_2020_Publication.csv)

-----------------

## JSfiddle, swagger document and a demo
```
https://jsfiddle.net/3a7Lkdfz
```
```
https://phaddressapi.sermeno.xyz/swagger/index.html
```
```
https://phaddressapi.sermeno.xyz/api/regions
```
Don't use my demo on production environment. I might turn it off if it consumes too much resources.

-----------------
## How Access Swagger Document

```
<ip:port>/swagger/index.html
```
Check swagger for all available endpoints and operation paramenters

------------------
## Run using docker
change the port 5002 if neccessary
```
docker run -p 5002:80 -d --restart unless-stopped allen456/phaddressapi:latest
```
-----------------

# Endpoints
Check swagger document for more info
## Regions
Data | Endpoint
------------ | -------------
All Regions | ```api/regions```
Specific Region | ```api/regions/{{id}}```
List of Provinces/Districts in a Region | ```api/regions/{{id}}/provincesDistricts```
## Provinces and Districts
Data | Endpoint
------------ | -------------
All Provinces and Districts | ```api/provincesDistricts```
Specific Provinces and Districts | ```api/provincesDistricts/{{id}}```
List of Cities and Municipalities in a Provinces and Districts | ```api/provincesDistricts/{{id}}/citiesMunicipalities```
## Cities and Municipalities
Data | Endpoint
------------ | -------------
All Cities and Municipalities | ```api/citiesMunicipalities```
Specific Cities and Municipalities | ```api/citiesMunicipalities/{{id}}```
List of Barangays in a Provinces/Districts | ```api/citiesMunicipalities/{{id}}/barangays```
## Barangays
Data | Endpoint
------------ | -------------
All Barangays | ```api/barangays```
Specific barangays | ```api/barangays/{{id}}```
## Maintenance
Data | Endpoint
------------ | -------------
Import | ```api/import``` 
Export | ```api/export```

Import CSV on a multipart/form-data with a field 'psgc' using the following format
- psgc code
- name
- old name

Export returns the following schema:
```
[
  {
    "regionId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "regionCode": "string",
    "regionName": "string",
    "provincesDistricts": [
      {
        "provinceDistrictId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "provinceDistrictCode": "string",
        "provinceDistrictName": "string",
        "provinceDistrictOldName": "string",
        "citiesMunicipalities": [
          {
            "cityMunicipalityId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
            "cityMunicipalityCode": "string",
            "cityMunicipalityName": "string",
            "cityMunicipalityOldname": "string",
            "barangays": [
              {
                "barangayId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                "barangayCode": "string",
                "barangayName": "string",
                "barangayOldname": "string",
                "cityMunicipalityId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
              }
            ],
            "provinceDistrictId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
          }
        ],
        "regionId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
      }
    ]
  }
]
```