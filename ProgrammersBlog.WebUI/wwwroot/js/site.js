function convertFirstLetterToUpperCase() {
    return text.charAt(0).toUpperCase() + text.slice(1);
}

function convertToShortDate(dateString) {
    return new Date(dateString).toLocaleDateString('tr-TR');
}