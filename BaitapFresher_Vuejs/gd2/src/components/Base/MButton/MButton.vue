<template>
  <button type="button" :class="ButtonCss" :tabindex="tab">
    {{text}}
  </button>
  <span v-if="toolTip" class="tool-tip">
            {{ toolTip }}
 </span>
</template>

<style>
.btn-button-openPopup-Employee{
  height: 40px;
  padding: 0 24px;
  border: none;
  text-align: center;
  background-color: #2CA01C;
  color: #ffff;
  border-radius: 4px;
  cursor: pointer;
  min-width: 100px;
  position: absolute;
  right: 0;
  font-weight: 700;
}.btn-button-openPopup-Employee:hover{
  background-color: #35BF22;

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
  height: 40px;
  padding: 0 24px;
  border: 2px solid #000;
  text-align: center;
  color: #000;
  border-radius: 20px;
  cursor: pointer;
  min-width: 100px;
  position: absolute;
  left: 30px;
  font-weight: 700;
  top: 0;
  background-color: #ffff;
}.btn-button-actionMutile:hover{
  background-color: #D2D3D6;
}.btn-button-filter{
  height: 40px;
  padding: 0 24px;
  border: 2px solid #000;
  text-align: center;
  color: #000;
  border-radius: 20px;
  cursor: pointer;
  min-width: 100px;
  position: absolute;
  left: 220px;
  font-weight: 700;
  top: 0;
  background-color: #ffff;
}.btn-button-filter:hover{
  background-color: #D2D3D6;  
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

}
</style>

<script>
export default {
  emit: ["custom-handle-click"],
  props:{
      text: String, 
      ButtonCss: String,
      tab: Number
  },
  methods: {
    
  },
};
</script>