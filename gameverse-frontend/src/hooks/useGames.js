// src/assets/js/UseGames.js
import { useEffect, useState } from "react";

const useGames = () => {
    const [games, setGames] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchGames = async () => {
            try {
                const token = localStorage.getItem("token");

                const response = await fetch("https://localhost:7214/api/games", {
                    headers: {
                        "Authorization": `Bearer ${token}`,
                        "Content-Type": "application/json"
                    }
                });

                if (!response.ok) {
                    throw new Error("Error al obtener los juegos");
                }

                const data = await response.json();
                setGames(data);
            } catch (error) {
                console.error("Error al cargar juegos:", error);
            } finally {
                setLoading(false);
            }
        };

        fetchGames();
    }, []);

    return { games, loading };
};

export default useGames;
