function serializeJsonForm() {
    var name = document.getElementById("myName").value;
    var lastname = document.getElementById("myLastname").value;
    var birthDay = document.getElementById("myBirthDay").value;
    var number = document.getElementById("myNumber").value;
    var mail = document.getElementById("myMail").value

    var form = {
        Name: name,
        LastName: lastname,
        BirthDay: birthDay,
        Number: number,
        Mail: mail
    };

    data = {
        id: "1",
        classJson: JSON.stringify(form)
    }

    var jsonString = JSON.stringify(data);
    

    var xhr = new XMLHttpRequest();
    xhr.open("POST", "url сервера", true);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.send(jsonString);
}
