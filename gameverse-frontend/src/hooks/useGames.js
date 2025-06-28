import { useEffect, useState } from 'react';
import axios from 'axios';

const useGames = () => {
    const [games, setGames] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        axios.get('http://localhost:5000/api/games') // Cambia si es otra URL
            .then((res) => {
                setGames(res.data);
                setLoading(false);
            })
            .catch((err) => {
                console.error('Error fetching games:', err);
                setLoading(false);
            });
    }, []);

    return { games, loading };
};

export default useGames;
