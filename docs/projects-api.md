# Catalogue Microservice 📚

Catalogue Microservice provides interface for working with book shop's catalogue. Main requests include:

**GET /categories** - for getting all categories

**GET /authors** - for getting all authors

**GET /books** - for searching for books (query includes page size and page index, as well as search parameters and filters, such as search string, cover type, availability status, price range, page range, categories, publishers and authors)

**POST /books** - for adding new book to catalogue (body includes book's details)

*Other requests allow to add, delete and edit categories and authors, as well as get individual categories, authors or books by identifier.*

# Cart Microservice 🛒

Cart Microservice provides interface for working with users' carts and favourites. Main requests include:

**GET /cart/user/{userId}** - for getting all items in user's cart by user identifier

**GET /favourites/user/{userId}** - for getting all items in user's favourites list by user identifier

**POST /cart** - for adding item to user's cart (body includes user identifier, item identifier and amount)

**POST /favourites** - for adding item to user's favourites list (body includes user identifier and item identifier)

**DELETE /cart** - for removing item from user's cart (query includes user identifier, item identifier and amount)

**DELETE /favourites** - for removing item from user's favourites list (query includes user identifier and item identifier)

*Other requests allow to get individual cart and favourite items by identifier.*

# Orders Microservice 🚚

Orders Microservice provides interface for working with orders. Main requests include:

**POST /orders** - for forming a new order (body includes order's details)

**PUT /orders** - for updating order status (body includes order's identifier and new status)

**GET /orders/user/{userId}** - for getting all user's orders list by user identifier

**POST /addresses** - for saving new address (body includes address' details)

**PUT /addresses** - for updating address (body includes new address' details)

**GET /addresses/user/{userId}** - for getting all user's addresses list by user identifier

*Other requests allow to delete orders and addresses, as well as get individual orders and addresses by identifier.*

# News Microservice 📰

News Microservice provides interface for working with news. Main requests include:

**GET /subscriptions/user/{userId}** - for getting user's subscriptions list by user identifier

**POST /subscriptions** - for subscribing user to publisher (body includes user identifier and publisher identifier)

**DELETE /subscriptions** - for unsubscribing user from publisher (query includes user identifier and publisher identifier)

**GET /news** - for getting all news sorted by date (query includes page size, page index and publishers isentifiers)

**POST /news** - for publishing news (body includes news' details)

**POST /publishers** - for adding new publishers accounts (body includes publisher's details)

*Other requests allow to edit and delete news and publidhers, as well as get individual news, publishers and subscriptions by identifier.*
