const wrapper = document.querySelector(".dictionaryWrapper"),
searchInput = wrapper.querySelector("input"),
infoText = wrapper.querySelector(".info-text"),
volumeIcon = wrapper.querySelector(".dictionaryWord i"),
removeIcon = wrapper.querySelector(".search span");
let audio;

function data(result, word){
    // if(result.title){
        // infoText.innerHTML = `Searching the meaning of <span>"${word}" </span>. Please, try to search for another word.`;
    // } else {
        console.log(result);
        wrapper.classList.add("active");
        
        // let definitions = result[0].meanings[0].definitions[0]
        // phonetics = `${result[0].meanings[0].partOfSpeech} / ${result[0].phonetics[0].text}/`
        //pass api
        // document.querySelector(".dictionaryWord p").innerText = result[0].word;
        // document.querySelector(".dictionaryWord span").innerText = phonetics;
        // document.querySelector(".dictionaryDefinition span").innerText = definitions.definition;
        // document.querySelector(".dictionaryExample span").innerText = definitions.example ;
        //obj audio
        audio = new Audio("https:" + result[0].phonetics[0].audio);

    // }
}

function fetchApi(word){
    infoText.style.color = "#000"
    // infoText.innerHTML = `Searching the meaning of <span>"${word}" </span>`;
    let url = `https://api.dictionaryapi.dev/api/v2/entries/en/${word}`
    fetch(url).then(response => response.json()).then(result => data(result,word)
    );
}

searchInput.addEventListener("keyup", e =>{
    let word = e.target.value.replace(/\s+/g, ' ');
    if(e.key == "Enter" && word){
        fetchApi(word);
    }
});

removeIcon.addEventListener("click",()=>{
    searchInput.value=""
    searchInput.focus();
    wrapper.classList.remove("active");
})