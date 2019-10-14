# Runpath.API

This is an API (using ASP .Net Core 2.2) getting albums and photos from the following links:
- http://jsonplaceholder.typicode.com/photos
- http://jsonplaceholder.typicode.com/albums

The API will return the albums with the associated photos.

There are 5 types of requests:
- http://localhost:53775/api/albums (returns all the albums and the associated photos)
- http://localhost:53775/api/albums?userId=2 (same as above but filters the results by the userid provided (userId = 2))
- http://localhost:53775/api/albums/1 (returns a specific album (id = 1) withh all the associated photos)
- http://localhost:53775/api/albums/100/photos (returns all the photos in the album with the id = 100)
- http://localhost:53775/api/albums/100/photos/4951 (returns the photo with the id = 4951 in the album with the id = 100)
