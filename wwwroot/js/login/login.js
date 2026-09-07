
function switchTheme() {
    const temaSalvo = localStorage.getItem("theme") || "light";
    document.documentElement.classList.remove("light", "dark");

    if (temaSalvo == "light") {
        localStorage.setItem("theme", "dark");
        document.documentElement.classList.add("dark");
    } else {
        localStorage.setItem("theme", "light");
        document.documentElement.classList.add("light");
    }
}

function loadTheme() {
    const temaSalvo = localStorage.getItem("theme") || "light";

    document.documentElement.classList.remove("light", "dark");
    document.documentElement.classList.add(temaSalvo);
}

window.addEventListener("load", loadTheme);