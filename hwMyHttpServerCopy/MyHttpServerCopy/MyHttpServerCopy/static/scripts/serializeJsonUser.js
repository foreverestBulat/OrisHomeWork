function serializeJsonUser() {
    var login = document.getElementById("login").value;
    var password = document.getElementById("password").value;

    var account = { Login: login, Password: password };

    var data = {
        id: "0",
        classJson: JSON.stringify(account)
    }

    var jsonString = JSON.stringify(data);
    console.log(jsonString);

    var xhr = new XMLHttpRequest();
    xhr.open("POST", "url server", true);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.send(jsonString);   
  }