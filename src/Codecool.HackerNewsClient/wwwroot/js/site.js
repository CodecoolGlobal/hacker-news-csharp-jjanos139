const topNews = document.querySelector("#top-news");
const newest = document.querySelector("#newest");
const jobs = document.querySelector("#jobs");
let page = 1;

let h1 = document.querySelector("#h1");
let authors = document.getElementsByClassName("author");

function loadData(link, page) {
    window.fetch("Home/" + link + "?page=" + page).then((response) => {
        response.json().then((data) => {
            let result = `<div>
                <button class="btn btn-primary" id="previous">Previous</button>
                <button class="btn btn-primary" id="next">Next</button>
                </div>
                <br>`;

            for (let i = 0; i < data.length; i++) {
                result += `
                <div class="jumbotron">
                    <p><strong>Title: </strong><a href="${data[i].url}">${data[i].title}</a></p>
                    <p class="author"><strong>Author: </strong>${data[i].user}</p>
                    <p><strong>Published: </strong>${data[i].time_ago}</p>
                </div>`;
            }

            const table = document.querySelector("#news");
            table.innerHTML = result;

            const next = document.querySelector("#next");
            const previous = document.querySelector("#previous");
            if (page === 1) {
                previous.setAttribute("disabled", "true");
            }
            if (page === 10) {
                next.setAttribute("disabled", "true");
            }

            if (h1.innerHTML === "Jobs") {
                next.setAttribute("hidden", "true");
                previous.setAttribute("hidden", "true");
                for (let i = 0; i < authors.length; i++) {
                    authors[i].innerHTML = "";
                }
            }
            previous.addEventListener("click", () => {
                page--;
                if (page < 10) {
                    next.removeAttribute("disabled");
                }
                loadData(h1.innerHTML, page);
            });
                
            next.addEventListener("click", () => {
                page++; 
                if (page > 1) {
                    previous.removeAttribute("disabled");
                }
                loadData(h1.innerHTML, page);
            });
        });
    });
}

topNews.addEventListener("click", () => {
    h1.innerHTML = "TopNews";
    loadData(h1.innerHTML, 1);
});

jobs.addEventListener("click", () => {
    h1.innerHTML = "Jobs";
    loadData(h1.innerHTML, 1);
});

newest.addEventListener("click", () => {
    h1.innerHTML = "Newest";
    loadData(h1.innerHTML, 1);
});



