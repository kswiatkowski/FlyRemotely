## FlyRemotely - JobPortal in ASP.NET MVC
### Live Demo: 
http://flyremotely.somee.com/

![](Screenshots/screenshot%20(1).png)

### Features:

**Job offer catalog:**
 - cache - downloading the list of categories and featured offers (24h cache) (home page)
 - asynchronous search results filter (ajax)
 - search suggestions (data-autocomplete) (ajax)
 - BreadCrumbs (MvcSiteMapProvider) - path on each subpage
 
**Offer management:**
 - featured offers - they are displayed in the footer of the subpage of each offer and as the first in the offer catalog
 - admin role is required to manage featured offers
 - adding offers 'to favorites' (after logging in)
 - management of favorite offers in profile
 - adding new offers and editing existing ones (including photos)
 - new / edited offers receive the status 'waiting' - to be approved by the admin
 - managing the status of offers (waiting, active, inactive, blocked)
 - users without admin rights have the option to change the offer status only to active / inactive

**User's account:**
 - registration and login system with data encryption (ASP Identity)
 - user roles with different permissions (admin)
 - asynchronous validation forms (client-side) (Microsoft.jQuery.Unobtrusive.Validation)
 - data models with attribute-based validation (DataAnnotations)
 - user data management (change assword)

**Summary:**
  - Entity Framework (CodeFirst), Bootstrap, RWD, Cache, ASP Identity, Linq, JavaScript, Ajax, jQuery, MvcSiteMapProvider, Unobtrusive Validation, and a lot more...

### Screenshots:
Screenshot 1. Job  offers catalog with asynchronous search engine (ajax):
![](Screenshots/screenshot%20(2).png)

Screenshot 2. Job offer details (application detail, add to favorites):
![](Screenshots/screenshot%20(3).png)

Screenshot 3. User data management and password change:
![](Screenshots/screenshot%20(4).png)

Screenshot 4. Adding / editing a job offer (with photo upload):
![](Screenshots/screenshot%20(5).png)

Screenshot 5. Offer status management, adding to featured (admin role required):
![](Screenshots/screenshot%20(6).png)

Screenshot 6. List of favorite offers (managing list items):
![](Screenshots/screenshot%20(7).png)

### Todos:

 - Add Night Mode
 
 ### Author:
 - LinkedIn: https://www.linkedin.com/in/kswiatkowski
 - Email: kaswiatkowski@gmail.com
