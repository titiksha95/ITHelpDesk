document.addEventListener("DOMContentLoaded", function () {
    loadWeather();
});

function loadWeather() {
    const temperatureElement =
        document.getElementById("weatherTemperature");

    const conditionElement =
        document.getElementById("weatherCondition");

    const iconElement =
        document.getElementById("weatherIcon");

    if (!navigator.geolocation) {
        temperatureElement.textContent = "Unavailable";
        conditionElement.textContent =
            "Location is not supported";
        return;
    }

    navigator.geolocation.getCurrentPosition(
        function (position) {
            const latitude = position.coords.latitude;
            const longitude = position.coords.longitude;

            fetchWeather(
                latitude,
                longitude,
                temperatureElement,
                conditionElement,
                iconElement);
        },
        function () {
            temperatureElement.textContent =
                "Location required";

            conditionElement.textContent =
                "Allow location for weather";

            iconElement.textContent = "📍";
        });
}

async function fetchWeather(
    latitude,
    longitude,
    temperatureElement,
    conditionElement,
    iconElement) {

    try {
        const apiUrl =
            `https://api.open-meteo.com/v1/forecast` +
            `?latitude=${latitude}` +
            `&longitude=${longitude}` +
            `&current=temperature_2m,weather_code` +
            `&timezone=auto`;

        const response = await fetch(apiUrl);

        if (!response.ok) {
            throw new Error("Weather request failed.");
        }

        const data = await response.json();

        const temperature =
            data.current.temperature_2m;

        const weatherCode =
            data.current.weather_code;

        const weather =
            getWeatherInformation(weatherCode);

        temperatureElement.textContent =
            `${Math.round(temperature)}°C`;

        conditionElement.textContent =
            weather.condition;

        iconElement.textContent =
            weather.icon;
    }
    catch (error) {
        temperatureElement.textContent =
            "Weather unavailable";

        conditionElement.textContent =
            "Please try again later";

        iconElement.textContent = "⚠️";

        console.error(error);
    }
}

function getWeatherInformation(code) {
    if (code === 0) {
        return {
            condition: "Clear sky",
            icon: "☀️"
        };
    }

    if (code === 1) {
        return {
            condition: "Mostly Sunny",
            icon: "🌤️"
        };
    }
    if (code === 2) {
        return {
            condition: "Partly Cloudy",
            icon: "🌤️"
        };
    }

    if (code === 3) {
        return {
            condition: "Cloudy",
            icon: "☁️"
        };
    }

    if (code === 45 || code === 48) {
        return {
            condition: "Foggy",
            icon: "🌫️"
        };
    }

    if (code >= 51 && code <= 67) {
        return {
            condition: "Rainy",
            icon: "🌧️"
        };
    }

    if (code >= 71 && code <= 77) {
        return {
            condition: "Snowy",
            icon: "❄️"
        };
    }

    if (code >= 80 && code <= 82) {
        return {
            condition: "Rain showers",
            icon: "🌦️"
        };
    }

    if (code >= 95) {
        return {
            condition: "Thunderstorm",
            icon: "⛈️"
        };
    }

    return {
        condition: "Current weather",
        icon: "🌤️"
    };
}