$(document).ready(function(){
    $(".combobox button").click(btnComboboxOnClick);
    $(".combobox .combobox-item").click(comboboxItemOnSelected);
})
function btnComboboxOnClick(){
    $(this.nextElementSibling).toggle();
}
function comboboxItemOnSelected(){
    //lay ra text va vaule vua chon
    const text = this.textContent;

    //biding text va luu value vua chon
    let parentElement = this.parentElement;
    let input = $(parentElement).siblings("input");
    $(input).val(text);
    $(parentElement).hide();
}