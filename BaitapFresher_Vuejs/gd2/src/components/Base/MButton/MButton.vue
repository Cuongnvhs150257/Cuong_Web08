<template>
  <button type="button" :class="ButtonCss" :tabindex="tab">
    {{text}} <div v-if="iconcss" :class="iconcss" ></div>
  </button>
  <span v-if="toolTip" class="tool-tip">
            {{ toolTip }}
 </span>
</template>

<script>
export default {
  emit: ["custom-handle-click"],
  props:{
      text: String, 
      ButtonCss: String,
      tab: Number,
      iconcss: String,
  },
  methods: {
    
  },
};
</script>

<style scoped>
:root {
  --icon: url("../../../assets/Resource/img/Sprites.64af8f61.svg");
}
.btn-button-openPopup-Employee{
  height: 38px;
  padding: 0 24px;
  border: none;
  text-align: center;
  background-color: #2CA01C;
  color: #ffff;
  border-radius: 4px;
  cursor: pointer;
  min-width: 60px;
  position: absolute;
  right: 0;
  font-weight: 700;
  margin-top: 2px;
}.btn-button-openPopup-Employee:hover{
  background-color: #35BF22;

}.btn-button-openPopup-Employee.c{
  height: 36px;
  padding: 0 12px;
  min-width: 100px;
}

.btn-button-openPopup {
  height: 40px;
  padding: 0 24px;
  border: none;
  text-align: center;
  background-color: #2CA01C;
  color: #ffff;
  border-radius: 20px;
  cursor: pointer;
  min-width: 100px;
  position: absolute;
  right: 0;
  font-weight: 700;
}.btn-button-openPopup:hover{
  background-color: #35BF22;

}.btn-button-product{
  height: 40px;
  padding: 0 24px;
  border: 2px solid #000;
  text-align: center;
  color: #000;
  border-radius: 20px;
  cursor: pointer;
  min-width: 100px;
  position: absolute;
  right: 110px;
  font-weight: 700;
}.btn-button-product:hover{
  background-color: #D2D3D6;
}.btn-button-utilities{
  height: 40px;
  padding: 0 24px;
  border: 2px solid #000;
  text-align: center;
  color: #000;
  border-radius: 20px;
  cursor: pointer;
  min-width: 100px;
  position: absolute;
  right: 330px;
  font-weight: 700;
}.btn-button-utilities:hover{
  background-color: #D2D3D6;
}.btn-button-actionMutile{
  height: 36px;
  padding: 0 15px;
  border: 2px solid #000;
  text-align: center;
  color: #000;
  border-radius: 20px;
  cursor: pointer;
  min-width: 100px;
  position: absolute;
  left: 30px;
  font-family: Misa Fonts Semibold;
  font-size: 13px;
  top: 0;
  background-color: #ffff;
  display: flex;
  align-items: center;
}.btn-button-actionMutile:hover{
  background-color: #D2D3D6;
}.btn-button-filter{
  height: 36px;
  padding: 0 15px;
  border: 2px solid #000;
  text-align: center;
  color: #000;
  border-radius: 20px;
  cursor: pointer;
  min-width: 60px;
  position: absolute;
  left: 220px;
  font-family: Misa Fonts Semibold;
  font-size: 13px;
  top: 0;
  background-color: #ffff;
  display: flex;
  align-items: center;
}.btn-button-filter:hover{
  background-color: #D2D3D6;  
}.icon-filter{
  background-image: var(--icon);
  background-position: -558px -359px;
  background-repeat: no-repeat;
  width: 15px;
  height: 15px;
  
}.btn-button-cancel{
  height: 36px;
  padding: 0 19px;
  border: 1px solid #bbb;
  text-align: center;
  color: #000;
  border-radius: 4px;
  cursor: pointer;
  min-width: 50px;
  position: absolute;
  left: 20px;
  font-weight: 700;
  top: 0;
  background-color: #ffff;
  margin-top: 10px;
}.btn-button-cancel:hover{
  background-color: #D2D3D6;  
}.btn-button-cancel.d{
  top: 150px;
  font-weight: 600;
}.btn-button-save{
    height: 36px;
    padding: 0px 21px;
    border: none;
    text-align: center;
    background-color: #2CA01C;
    color: #ffff;
    border-radius: 4px;
    cursor: pointer;
    min-width: 100px;
    position: absolute;
    right: 20px;
    margin-top: 10px;
    font-weight: 700;
}.btn-button-save:hover{
  background-color: #35BF22;

}.btn-button-save.b{
  min-width: 50px;
}.btn-button-save.c{
  min-width: 50px;
  margin-top: 0;
}.btn-button-save-emp{
  height: 36px;
    padding: 0px 21px;
    border: none;
    text-align: center;
    background-color: #2CA01C;
    color: #ffff;
    border-radius: 4px;
    cursor: pointer;
    min-width: 100px;
    position: absolute;
    right: 35px;
    margin-top: 15px;
    font-weight: 700;
}.btn-button-save-emp:hover{
  background-color: #35BF22;

}.deletemultple{
    width: 200px;
    height: 40px;
    padding: 11px 19px;
    background-color: #fff;
    color: #000;
    border: 2px solid #000;
    border-radius: 30px;
    display: flex;
    text-align: center;
    cursor: pointer;
    font-weight: 700;
}.deletemultple:hover{
        background-color: rgb(236, 238, 241);
}.deletemultple-icon{
        width: 24px;
        height: 24px;
        background-image: var(--icon);
        background-position: -561px -357px;
        background-repeat: no-repeat;
        position: absolute;
        top: 10px;
        right: 0;
        cursor: pointer;
}
</style>

