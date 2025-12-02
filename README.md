# Rick and Morty Explorer

A simple web application to search for **Rick and Morty** episodes and view the characters in each episode. The system uses **Angular** for the frontend and **.NET** for the backend. It is fully **Dockerized**, so you can get it running with a single command.

---

## 🚀 Features

- Search episodes by name.
- Display characters for the selected episode in cards.
- Sort characters alphabetically (ascending/descending).
- Fully dockerized for easy setup.

---

## 🛠 Technologies Used

### Frontend

- Angular (latest stable version)
- TailwindCSS 3
- RxJS (for reactive programming)
- HTML5 / CSS3 / TypeScript

### Backend

- .NET 9 Web API
- C#
- HttpClient for external API calls
- Dependency Injection
- Dockerized

---

## 📦 Docker

The project is fully dockerized. To run:

```
git clone https://github.com/GustavoSelhorstMarconi/RickAndMorty.git
cd .\RickAndMorty\
docker compose up --build
```

## Next Steps
- Use DTOs in the backend to return only necessary information between layers, avoiding exposing internal entities directly.
- Implement unit and integration tests for backend services and Angular components.
