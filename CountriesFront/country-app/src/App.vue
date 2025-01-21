<template>
  <nav>
    <router-link to="/">Countries App</router-link>
  </nav>
  <router-view />
</template>

<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #404952;
}

nav {
  padding: 30px;

  a {
    font-weight: bold;
    color: #3c4650;

    &.router-link-exact-active {
      color: #bdcbcd;
    }
  }
}
</style>
