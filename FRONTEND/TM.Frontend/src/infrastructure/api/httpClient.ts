import axios from "axios";

export const httpClient = axios.create({
    baseURL: "http://185.215.166.32:5000",
    headers: {
        "Content-Type": "application/json",
    },
    timeout: 10000,
});