<template>
  <div class="cover-popup">
    <div class="popupask">
      <div class="popupask-top">
        <div class="popupask-icon icon-ask"></div>
        <div class="popupask-icon-label-ask">
          <label class="label-ask"
            >Bạn có thực sự muốn xóa Nhân viên
            {{ getEmployeeCode }} không?</label
          >
        </div>
      </div>
      <div class="popupask-bottom">
        <button class="popupask-bottom-right" @click="popupaskcance">Không</button>
        <MButtonAgree @click="agreedelete" />
      </div>
    </div>
  </div>
</template>

<script>
import MButtonAgree from "./MButtonAgree.vue";

export default {
  props: {
    getEmployeeCode: String,
  },
  methods: {
    //hàm nhấn nút hủy xóa nhân viên
    popupaskcance() {
      this.$emit("popup-ask-cance");
    },
    //hàm đồng ý xóa nhân viên
    agreedelete() {
      this.$emit("agree-delete-click");
    },
  },
  components: { MButtonAgree },
};
</script>

<style>
:root {
  --icon: url("../../assets/Resource/img/Sprites.64af8f61.svg");
}
.cover-popup {
  position: fixed;
  width: 100%;
  height: 100vh;
  right: 0;
  left: 0;
  top: 0;
  bottom: 0;
  background-color: rgba(12, 12, 12, 0.3);
  z-index: 1000;
}
.popupask {
  width: 450px;
  height: 180px;
  position: relative;
  top: 40%;
  margin: 0 auto;
  background-color: #fff;
}
.popupask-top {
  width: 100%;
  height: 100px;
  display: flex;
  position: relative;
}
.popupask-icon {
  width: 50px;
  height: 49px;
  margin-left: 50px;
  margin-top: 50px;
  border-bottom: 1px solid;
}
.icon-ask {
  background-image: var(--icon);
  background-position: -598px -463px;
  background-repeat: no-repeat;
}
.popupask-icon-label-ask {
  width: 320px;
  margin-top: 30px;
  border-bottom: 1px solid;
  padding-top: 10px;
  line-height: 25px;
}
.label-ask {
  color: #6b6b6b;
  line-height: 20px;
}
.popupask-icon2 {
  width: 50px;
  height: 50px;
  background-color: #fff;
  border: none;
}
.icon-cance {
  background-image: var(--icon);
  background-position: -128px -131px;
  background-repeat: no-repeat;
}
.popupask-bottom-right {
  height: 38px;
  width: 80px;
  border: 1px solid black;
  text-align: center;
  color: black;
  font-weight: bold;
  background-color: #ffff;
  border-radius: 4px;
  cursor: pointer;
  margin-top: 20px;
  margin-left: 50px;
  position: absolute;
  left: 0;
}
</style>


