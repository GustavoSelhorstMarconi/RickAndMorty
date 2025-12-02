FROM node:lts-slim AS build
WORKDIR /src

RUN npm install -g @angular/cli@latest

COPY package*.json ./
RUN npm ci

COPY . ./

ARG API_URL
RUN echo "export const environment = { production: true, apiBaseUrl: '$API_URL' };" > src/environments/environment.prod.ts

RUN ng build --configuration=production

FROM nginx:stable
EXPOSE 80

RUN rm -rf /usr/share/nginx/html/*

COPY --from=build /src/dist/rick-and-morty-app/browser/ /usr/share/nginx/html/

CMD ["nginx", "-g", "daemon off;"]
