<template>
  <div class="combobox">
    <input class="combobox-input" type="text" v-model="this.UnitSle.unitName" :class="{ 'combobox-input-red': !inValueCombox, 'combobox-input-green': !inValueChange }" :tabindex="tab"/>
    <button @click="btnComboboxOnClick"></button>
    <div class="combobox-data">
      <div class="combobox-item" v-for="u in UnitItem" :key="u.unitID" @click="selectedUnit(u)" >
          <div class="combobox-item-left" >{{u.unitCode}}</div>
          <div class="combobox-item-right">{{u.unitName}}</div>
      </div>
      
    </div>
  </div>
</template>

<script>
import configs from '../../configs/index';
export default {

  props:["EmployeeUnit","modelValue","inValueCombox","tab"],

  methods:{
        
        /**
         * hàm mở combobox 
         * Nguyễn Văn Cương 05/10/2022
         */
        btnComboboxOnClick() {
            this.OpenComboxbox = !this.OpenComboxbox;
            if(this.OpenComboxbox){
                this.loadUnit();
            }else{
                this.UnitItem = {};
            }
        },

        /**
         * hàm lấy toàn bộ đơn vị
         * Nguyễn Văn Cương 05/10/2022
         */
        loadUnit(){
            fetch(configs.baseURLUnit, {
            method: "GET",
      })
        .then((res) => res.json())
        .then((data) => {
          this.UnitItem = data;
        })
        .catch((res) => {
          console.log(res);
        });
        },

        /**
         * hàm chọn đơn vị
         * Nguyễn Văn Cương 05/10/2022
         */
        selectedUnit(un){
          this.UnitSle = un;
          this.UnitItem = {};
          this.$emit("get-unitid", un.unitID);
          this.inValueChange = this.inValueCombox;
        }


   
  }, data(){
        var UnitSle = {
          //hiện thị tên đơn vị của nhân viên khi mở popup sửa nhân viên
            unitName: this.EmployeeUnit 
        };
        console.log(this.EmployeeUnit);
        return{
            OpenComboxbox: false, //gọi combobox
            UnitItem: {}, //lưu giá trị từng đơn vị
            UnitSle, //lưu id đơn vị đã chọn
            inValue: { //nổi bật đơn vị đã chọn
              type: Boolean,
              default: true,
            },
            inValueChange: {
                type: Boolean,
                default: true,
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
  height: 28px;
  width: 100%;
  box-sizing: border-box;
  display: flex;
  position: relative;
  margin-top: 5px;
}
.combobox-input{
  width: 100%;
  height: 27px;
  float: left;
  height: 100%;
  border: none;
  outline: none;
  padding-left: 10px;
  border: 1px solid #bbbbbb;
  border-radius: 3px;
}
.combobox-input:focus{
  height: 28px;
  border: 2px solid #50B83C;
  border-radius: 4px;
  outline: none;
  z-index: 2;
}

.combobox button{
  position: absolute;
  border: none;
  right: 1px;
  top: 2px;
  width: 40px;
  height: 27px;
  flex-shrink: 0;
  flex-basis: 40px;
  cursor: pointer;
  margin-right: 1px;
  background: #fff;
  z-index: 2;
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
  top: 29px;
  width: 99%;
  z-index: 1;
  border-radius: 4px ;
  border-right: 1px solid #bbbb;
  border-left: 1px solid #bbbb;
  border-bottom: 1px solid #bbbb;
  background-color: #fff;
  
}
.combobox-item {
  height: 32px;
  width: 370px;
  display: flex;
  align-items: center;
  margin-left: 8px;
  padding-left: 8px;
  font-size: 13px;
  background-color: #fff;
 
}.combobox-input-red {
  border: 1px solid #ff0000;
  border-radius: 3px;
  z-index: 2;
}.combobox-input-green{
  border: 2px solid #50B83C;
}
.combobox-item-left{
    width: 100px;
}
.combobox-item:hover {
  background-color: rgba(80,184,60,0.1);
  color: #000;
  border-radius: 4px;
}::-webkit-scrollbar {
  height: 8px;
  width: 8px;
}
::-webkit-scrollbar-track {
  background: #f1f1f1;
  border-radius: 4px;
}

/* Handle */
::-webkit-scrollbar-thumb {
  background: #bbb;
  border-radius: 4px;
  cursor: pointer;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #7c869c;
}.combobox-item-checked{
    background-color: #50b83c;
    color: #fff;
}
</style>