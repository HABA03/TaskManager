import axios from "axios";

export const httpClient = axios.create({
    baseURL: "http://localhost:5192",
    headers: {
        "Content-Type": "application/json",
    },
    timeout: 10000,
});