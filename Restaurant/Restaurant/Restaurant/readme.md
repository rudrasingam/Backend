MEGET VIGTIGT TIL AT BENYTTE DETTE:
I skal ind på appsettings.development og skrive jeres DB, sa og password, så man kan connecte vores EF CORE til jeres database. 

Derefter skal i ind på connected services, så i kan migrate og dermed opdatere database. 



# Restaurant Backend API

## Oversigt

Denne backend API fungerer som datalag for en restaurantapplikation, der tilbyder grænseflader til håndtering af reservationer, bestillinger, menupunkter, personale og bordopsætninger. API'en er bygget med ASP.NET Core og bruger Entity Framework Core.

## Modeller
Modeller over tabeller. Skal kigges igennem fælles.  

-   **Guest (Gæst)**: Repræsenterer en kunde i restauranten. Indeholder personlige oplysninger og navigationsproperties til reservationer og bestillinger.
-   **Reservation**: Administrerer alle aspekter af bordreservationer. Forbundet med `Guest` og `Table`.
-   **Table (Bord)**: Repræsenterer et fysisk bord i restauranten. Indeholder egenskaber som kapacitet og er forbundet med reservationer.
-   **Order (Bestilling)**: Sporer ordrer afgivet af gæster. Indeholder ordredetaljer og er associeret med `Guest`.
-   **OrderItem (Bestillingselement)**: Repræsenterer individuelle elementer inden for en ordre, forbundet med `MenuItem`.
-   **MenuItem (Menupunkt)**: Oplysninger om menupunkter tilgængelige for bestilling.
-   **Staff (Personale)**: Information om restaurantens personale, inklusive roller, kontaktoplysninger osv.

## Controllere
Vi skal opdatere vores controllere således at endpoints passer med react, så vi kan kommunikere med https. 

-   **GuestsController**: Håndterer CRUD-operationer for gæster.
-   **ReservationsController**: Administrerer reservationsdata.
-   **TablesController**: Leverer endepunkter til bordstyring.
-   **OrdersController**: Håndterer alle aktiviteter relateret til bestillinger.
-   **OrderItemsController**: Administrerer individuelle elementer inden for en bestilling.
-   **MenuItemsController**: CRUD-operationer for menupunkter.
-   **StaffController**: Administrerer personaledetaljer.

