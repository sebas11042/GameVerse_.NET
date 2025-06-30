import { useEffect, useState } from 'react';

export function useUserLibrary() {
    const [purchasedGames, setPurchasedGames] = useState([]);
    const [rentedGames, setRentedGames] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const token = localStorage.getItem("token");
        const userId = localStorage.getItem("userId"); 

        if (!token || !userId) return;

        const fetchPurchased = fetch(`https://localhost:7214/api/Shoppings/history/${userId}`, {
            headers: { Authorization: `Bearer ${token}` }
        }).then(res => res.json());

        const fetchRented = fetch(`https://localhost:7214/api/Rentals/user/${userId}`, {
            headers: { Authorization: `Bearer ${token}` }
        }).then(res => res.json());

        Promise.all([fetchPurchased, fetchRented])
            .then(([purchased, rented]) => {
                setPurchasedGames(
                    purchased.flatMap(p => p.purchaseDetails.map(d => d.idGameNavigation))
                );
                setRentedGames(
                    rented.flatMap(r => r.rentalDetails.map(d => ({
                        ...d.idGameNavigation,
                        expireDate: d.expireDate
                    })))
                );
                setLoading(false);
            })
            .catch(error => {
                console.error("Error al cargar juegos:", error);
                setLoading(false);
            });
    }, []);

    return { purchasedGames, rentedGames, loading };
}
