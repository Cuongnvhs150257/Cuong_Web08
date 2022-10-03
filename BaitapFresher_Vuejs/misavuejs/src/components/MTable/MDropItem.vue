<template>
  <div class="droptbl">
    <button class="droptbl" @click="btnDropClick"></button>
    <div class="drop-datatbl" v-if="OpenDropbox === true">
      <div class="drop-itemtbl"  @click="SelectedEdit(1)">Nhân bản</div>
      <div class="drop-itemtbl"  @click="SelectedEdit(2)">Xóa</div>
      <div class="drop-itemtbl" @click="SelectedEdit(3)">Ngưng sử dụng</div>
    </div>
  </div>
</template>

<script>
export default {
  methods:{
      
        btnDropClick() {
            this.OpenDropbox = !this.OpenDropbox;
            // this.inValue = true;
            console.log(this.OpenDropbox);
        },
        SelectedEdit(value){
          this.$emit("edit-value", value);
          this.OpenDropbox = false;
        }
    
  },
  data(){
        return{
            OpenDropbox: false,
        }
    }
};
</script>


<style>
:root{
    --icon: url("../../assets/Resource/img/Sprites.64af8f61.svg");
}
.droptbl {
  height: 30px;
  width: 100%;
  box-sizing: border-box;
  display: flex;
  position: relative;
}

.droptbl .droptbl{
  position: absolute;
  border: none;
  right: 1px;
  width: 30px;
  height: 28px;
  flex-shrink: 0;
  flex-basis: 40px;
  cursor: pointer;
  margin-right: 1px;
  background: #fff;
  margin: 0;
}
.droptbl .droptbl{
    background-image: var(--icon);
    background-position: -1067px -26px;
    background-repeat: no-repeat;
}.droptbl .drop:hover{
    background-color: #dddd;
}
.drop-datatbl {
  position: absolute;
  top: 32px;
  width: 49px;
  right: 50px;
  background-color: #fff;
  z-index: 1;
  border-bottom: 1px solid #bbbb;

}
.drop-itemtbl {
  height: 32px;
  width: 100px;
  display: flex;
  align-items: center;
  padding-left: 10px;
  background-color: #fff;
  font-size: 13px;
  color: #000;
}

.drop-itemtbl:hover {
  color: #50b83c;
  background-color: #EBEDF0;
}.drop-data-hide{
    display: none;
}
</style>