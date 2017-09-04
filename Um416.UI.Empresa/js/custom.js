// Javascript Date to Csharp Date and DateTime
String.prototype.toCsDate = function () {
    return moment(this, 'DD/MM/YYYY').format().substring(0, 19);
}

String.prototype.toCsDateTime = function () {
    return moment(this, 'DD/MM/YYYY HH:mm').format().substring(0, 25);
}

// Chasrp Date to Javascript Date
String.prototype.toJsDate = function () {
    return moment(this.substring(0, 10), 'YYYY-MM-DD').format('DD/MM/YYYY');
}

String.prototype.toJsDateTime = function () {
    return moment(this.substring(0, 16), 'YYYY-MM-DD HH:mm').format('DD/MM/YYYY HH:mm');
}