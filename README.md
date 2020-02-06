# ServiceManual
Service Manual .NET Core API Demo

Osoite API:n Swagger Dokumentaatioon:  
http://servicemanual.jonneokkonen.com/swagger/index.html

API:n Base URL: http://servicemanual.jonneokkonen.com/api/v1

# API

API:n kaikki GET-toiminnot, joilla vain luetaan dataa ovat täysin avoimet. Datan muuttaviin toimintoihin kuten lisäys, luokkaus ja poisto tarvitaan API-avain. Testausta varten olen toimittanut sen tarvittaville henkilöille.

## Tarkempaa tietoa toiminnoista

![](doc/Swagger-frontpage.JPG)

## Device
API:ssa on tällä hetkellä toteutettu ylimääräisinä toimintoina pari hakutoimintoa myös laitteille. Näiden toimintojen avulla voidaan hakea tietokannasta kaikki laitteet tai vain yksi tietty laite.

## Maintenance Task

Huoltotehtävien noutoon on toteutettu kaikki perustoiminnot, kuten kaikkien tehtävien hakeminen, tietyn tehtävän hakeminen, kaikkien tehtävien hakeminen tietylle laitteelle sekä tehtävien lisääminen, muokkaaminen ja poistaminen.   
Huoltotehtävien listaus on suodatettu kriittisyyden mukaan korkeimmasta pienempään sekä sen jälkeen kirjausajankohdan mukaisesti uusimmasta vanhimpaan.

# Tietokanta

Tietokanta on toteutettu MySQL, johon yhteyden loin itse Database luokan avulla, hyödyntämällä MySql.Data.MySqlClient kirjastoa. Parempi tapa yhteyksien luomiseen ja tietokanta operaatioihin olisi ollut Entity Framework Core ja EF Migraatiot. En kuitenkaan kerinnyt tutustua kyseiseen kirjastoon tarpeeksi ajoissa, joka ei ollut aiemmin minulle tuttu, ja tästä syystä päätin tehdä tietokanta yhteydet itse manuaalisesti.

## ER-kaavio
![](doc/ServiceManual_ER_kaavio.png)

## Device (Laite)
DeviceID (LaiteID)  
Name (Nimi)  
Year (Vuosi) Muoto:'YYYY'  
Type (Tyyppi) 

## Maintenance Task (Huoltotehtävä)
TaskID (TehtäväID)  
DeviceID (LaiteID)  
Created (Lisäyspäivämäärä) Muoto:'YYYY-mm-dd HH:mm:ss'  
Priority (Kriittisyys) Vaihtoehdot: 1, 2 tai 3 (1: Lievät, 2: Tärkeät, 3: Kriittiset)  
State (Tila) Vaihtoehdot: 0 tai 1 (0: Huollettu, 1: Avoin)  
Description (Kuvaus)

[Linkki SQL-tiedostoon](doc/ServiceManual.sql)