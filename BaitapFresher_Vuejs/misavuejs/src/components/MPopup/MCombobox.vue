<template>
  <div class="combobox">
    <input type="text" />
    <button @click="btnComboboxOnClick"></button>
    <div class="combobox-data" v-for="u in UnitItem" :key="u.unitID">
      <div class="combobox-item">
          <div class="combobox-item-left">{{e.unitCode}}</div>
          <div class="combobox-item-right">{{e.unitName}}</div>
      </div>
      <!-- 
      <div class="combobox-item">
          <div class="combobox-item-left">HA NOI</div>
          <div class="combobox-item-right">Chi nhánh Hà Nội</div>
      </div>
      <div class="combobox-item">
          <div class="combobox-item-left">HCM</div>
          <div class="combobox-item-right">Chi nhánh HCM</div>
      </div> -->

      
      
    </div>
  </div>
</template>

<script>
export default {
  methods:{

        
        btnComboboxOnClick() {
            this.OpenComboxbox = true;
            this.loadUnit();
            
        },

        loadUnit(){
            fetch("http://localhost:17703/api/Unit", {
            method: "GET",
      })
        .then((res) => res.json())
        .then((data) => {
          this.UnitItem = data;
          console.log(this.UnitItem);
        })
        .catch((res) => {
          console.log(res);
        });
        },


    data(){
        return{
            OpenComboxbox: false,
            UnitItem: {}
        }
    }
  }
};
</script>


<style>
:root{
    --icon: url("../../assets/Resource/img/Sprites.64af8f61.svg");
}
.combobox {
  height: 30px;
  width: 100%;
  box-sizing: border-box;
  display: flex;
  position: relative;
}
.combobox input {
  width: calc(100%);
  float: left;
  height: 100%;
  border: 1px solid #bbbbbb;
  outline: none;
  margin-top: 5px;
  padding-left: 10px;
}
.combobox input:focus {
  border: 2px solid #019160;
  outline: none;
}

.combobox button{
  position: absolute;
  border: none;
  right: 1px;
  top: 1px;
  width: 40px;
  height: 28px;
  flex-shrink: 0;
  flex-basis: 40px;
  cursor: pointer;
  margin-top: 7px;
  margin-right: 1px;
  background: #fff;
}
.combobox button{
    background-image: var(--icon);
    background-position: -1062px -26px;
    background-repeat: no-repeat;
}.combobox button:hover{
    background-color: #dddd;
}
.combobox-data {
  position: absolute;
  top: 40px;
  width: 100%;
  z-index: 1;

}
.combobox-item {
  height: 32px;
  display: flex;
  align-items: center;
  padding-left: 10px;
  border: 1px solid #bbbbbb;
  font-weight: bold;
  font-size: 13px;
  background-color: #fff;
}
.combobox-item-left{
    width: 100px;
}
.combobox-item:hover {
  background-color: #50b83c;
  color: #fff;
}
</style>