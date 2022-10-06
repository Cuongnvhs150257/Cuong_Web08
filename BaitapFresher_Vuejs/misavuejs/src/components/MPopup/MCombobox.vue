<template>
  <div class="combobox">
    <input class="combobox-input" type="text" v-model="this.UnitSle.unitName"/>
    <button @click="btnComboboxOnClick"></button>
    <div class="combobox-data" >
      <div class="combobox-item" v-for="u in UnitItem" :key="u.unitID" @click="selectedUnit(u)" >
          <div class="combobox-item-left" >{{u.unitCode}}</div>
          <div class="combobox-item-right">{{u.unitName}}</div>
      </div>
      <!-- 
      :class="{ 'combobox-item-checked': !inValue }" 
       -->

      
      
    </div>
  </div>
</template>

<script>
export default {

  props:["EmployeeUnit","modelValue"],

  methods:{
        
        //hàm mở combobox
        btnComboboxOnClick() {
            this.OpenComboxbox = !this.OpenComboxbox;
            if(this.OpenComboxbox){
                this.loadUnit();
            }else{
                this.UnitItem = {};
            }
        },
        //hàm lấy toàn bộ đơn vị
        loadUnit(){
            fetch("https://localhost:44335/api/v1/Unit", {
            method: "GET",
      })
        .then((res) => res.json())
        .then((data) => {
          this.UnitItem = data;
                //nổi bật đơn vị đã chọn
                if(this.UnitSle.unitName == "Phòng kế toán"){
                   this.inValue = false;
                   
                }
        })
        .catch((res) => {
          console.log(res);
        });
        },
        //hàm chọn đơn vị
        selectedUnit(un){
          this.UnitSle = un;
          this.UnitItem = {};
          this.$emit("get-unitid", un.unitID);
        }


   
  }, data(){
        var UnitSle = {
            unitName: this.EmployeeUnit //hiện thị tên đơn vị của nhân viên khi mở popup sửa nhân viên
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
  margin-top: 5px;
  border: 1px solid #bbbbbb;
}
.combobox-input{
  width: calc(100%);
  float: left;
  height: 100%;
  border: none;
  outline: none;
  padding-left: 10px;
}
.combobox-input:focus {
  height: 28px;
  border: 2px solid #019160;
  outline: none;
  z-index: 1;
}

.combobox button{
  position: absolute;
  border: none;
  right: 1px;
  top: 1px;
  width: 40px;
  height: 27px;
  flex-shrink: 0;
  flex-basis: 40px;
  cursor: pointer;
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
  top: 28px;
  width: 99%;
  z-index: 1;
   overflow-y: scroll ;
   border: 1px solid #bbbb;
  
}
.combobox-item {
  height: 32px;
  display: flex;
  align-items: center;
  padding-left: 10px;
  font-size: 13px;
  background-color: #fff;
 
}
.combobox-item-left{
    width: 100px;
}
.combobox-item:hover {
  background-color: #50b83c;
  color: #fff;
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