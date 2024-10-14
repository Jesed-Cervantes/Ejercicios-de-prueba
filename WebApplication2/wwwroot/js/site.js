async function getFibonacci() {
    const n = document.getElementById('inputN').value;
    const url = `/api/Fibonacci/${n}`;

    try {
        const response = await fetch(url);
        const data = await response.json();
        document.getElementById('result').innerText = JSON.stringify(data);
    } catch (error) {
        console.error('Error:', error);
        document.getElementById('result').innerText = 'Error al obtener la secuencia.';
    }
}