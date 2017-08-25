# Band Tracker #
## By Jonathan Stein ##
## _A band tracking site that lets the user add bands to venues, and venues to bands_ ##
___


### User Story



- Full CRUD functionality for Venues. Create, Read (show all, show single), Update, Delete.

- Allow a user to create Bands that have played at a Venue. Don't worry about building out updating or deleting for bands.

- There is a many-to-many relationship between bands and concert venues, so a venue can host many bands, and a band can play at many venues. Create a join table to store these relationships.

- When a user is viewing a single concert venue, list out all of the bands that have played at that venue so far and allow them to add a band to that venue. Create a method to get the bands who have played at a venue, and use a join statement in it.

- When a user is viewing a single Band, list out all of the Venues that have hosted that band and allow them to add a Venue to that Band.



### Technical Specs

| App Behavior | Input | Actual |
|----|----|----|  
|  Get a list of all bands from DB | List of all bands | List of all bands from DB |
|  Save band to database  |  TestBand  |  TestBand  |
|  Find band by Id | Band Id | Band Id from DB |
|  Displays list of all venues for band | TestBand | All venues for band |
| Update specific band name | BandNewName | BandList with updated band name |
| Delete specific band | BandToDelete  | BandList without BandToDelete  |
| Search by Band name | TestBandName | TestBand |
|  Get a list of all venues from DB | List of all venues | List of all venues from DB |
|  Save venue to database  |  TestVenue  |  TestVenue  |
|  Find venue by Id | Venue Id | Venue Id from DB |
|  Displays list of all band for venue | TestVenue | All venues for venue |
| Update specific venue name | VenueNewName | VenueList with updated venue name |
| Delete specific venue | VenueToDelete  | VenueList without VenueToDelete  |
| Search by Venue name | TestVenueName | TestVenue |

### _Content_ ###

Index.cshtml:
- Splash page

Bands.cshtml
- View list of bands, add bands, add/remove venues to bands

Venues.cshtml
- View list of venues, add venues, add/remove bands to venues

BandDetails.cshtml
- View/change band details, remove band

VenueDetails.cshtml
- View/change venue details, remove venue


Other:
- Client.cs
  - Model for band class
- HomeController.cs
  - Getting and Posting routes
- Layout.cshtml
  - CSS layout and script importing
- Styles.css
  - CSS management

### _How to use_ ###

1. Download project from GitHub: https://github.com/JStein92/BandTracker
2. Run HTML in preferred browser
3. Follow instructions on page
  - Create new bands/venues
  - Add venues to bands and bands to venues
  - Remove bands and venues and relationships between them


### _Database Setup_ ###

CREATE DATABASE band_tracker;

mysql> CREATE TABLE bands (id serial PRIMARY KEY, name VARCHAR(255), genre VARCHAR(255), image varchar(255));

mysql> CREATE TABLE venues (id serial PRIMARY KEY, name VARCHAR(255), address VARCHAR(255), capacity INT);

CREATE DATABASE band_tracker_test;
USE band_tracker_test;

mysql> CREATE TABLE bands (id serial PRIMARY KEY, name VARCHAR(255), genre VARCHAR(255), image varchar(255));

mysql> CREATE TABLE venues (id serial PRIMARY KEY, name VARCHAR(255), address VARCHAR(255), capacity INT);
