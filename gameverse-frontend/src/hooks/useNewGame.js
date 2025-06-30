const createGame = async (gameData) => {
    try {
        const token = localStorage.getItem("token");

        const response = await fetch("https://localhost:7214/api/games", {
            method: "POST",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(gameData)
        });

        if (!response.ok) {
            const errorText = await response.text();
            console.error("Respuesta del backend:", errorText);
            throw new Error("Error al guardar el juego");
        }

        const data = await response.json();
        return data;

    } catch (error) {
        console.error("Error al crear juego:", error);
        throw error;
    }
};

export default createGame;
