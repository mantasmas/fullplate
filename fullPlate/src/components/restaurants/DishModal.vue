<template>
  <transition name="modal">
    <div class="modal-mask">
      <div class="modal-wrapper">
        <div class="modal-container">
          <div class="deletion-confirmation">
            <form v-on:submit.prevent="handleAdd()">
              <label for="name">Dish name</label>
              <input id="name" type="text" name="name" v-model="name" required/>
              <p>Dish type</p>
              <div>
                <label for="radio-soup">Soup</label>
                <input id="radio-soup" type="radio" value="1" v-model="dishType"/>
                <label for="radio-main">Main dish</label>
                <input id="radio-main" type="radio" value="2" v-model="dishType"/>
              </div>
              <label for="isVegetarian">Vegetarian</label>
              <input id="isVegetarian" type="checkbox" v-model="isVegetarian"/>
              <div>
                <input type="submit" class="btn-secondary js-confirm" value="Add dish"/>
                <button type="button" class="btn-link" data-modal-control="close" @click="handleCancel">Cancel</button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </transition>
</template>

<script>
  import { mapGetters } from 'vuex';

  export default {
    name: 'dish-modal',
    props: ['toggleModal', 'restaurantId'],
    data () {
      return {
        name: '',
        price: 0,
        dishType: 1,
        isVegetarian: false,
        money: {
          decimal: ',',
          thousands: '.',
          prefix: '€ ',
          suffix: '',
          precision: 2,
          masked: false
        }
      };
    },
    methods: {
      handleAdd () {
        const dishData = {
          name: this.name,
          price: this.price,
          dishType: this.dishType,
          isVegetarian: this.isVegetarian
        };

        this.$store.dispatch('addNewDish', {restaurantId: this.restaurantId, dishData});
        this.toggleModal();
      },
      handleCancel () {
        this.toggleModal();
      }
    },
    computed: {
      ...mapGetters(['restaurantDeleteId'])
    }
  };
</script>

<style>
  .modal-mask {
    position: fixed;
    z-index: 9998;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, .5);
    display: table;
    transition: opacity .3s ease;
  }

  .modal-wrapper {
    display: table-cell;
    vertical-align: middle;
  }

  .modal-container {
    width: 300px;
    margin: 0px auto;
    padding: 20px 30px;
    background-color: #fff;
    border-radius: 2px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, .33);
    transition: all .3s ease;
    font-family: Helvetica, Arial, sans-serif;
  }

  .modal-header h3 {
    margin-top: 0;
    color: #42b983;
  }

  .modal-body {
    margin: 20px 0;
  }

  .modal-default-button {
    float: right;
  }

  .modal-enter {
    opacity: 0;
  }

  .modal-leave-active {
    opacity: 0;
  }

  .modal-enter .modal-container,
  .modal-leave-active .modal-container {
    -webkit-transform: scale(1.1);
    transform: scale(1.1);
  }
</style>
