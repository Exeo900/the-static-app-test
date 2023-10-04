<script lang="ts">
  import { onMount } from "svelte";

  let randomGuid: string;

  async function getGuid() {
    const response = await fetch(`/api/GetRandomGuid`);
    randomGuid = await response.text();
  }

  onMount(async () => {
    getGuid();
  });
</script>

<main>
  <h1>Welcome to this website</h1>
  <p>Detta är ju en SWA, ingen SSR här inte...</p>

  <p>Detta är en random guid från en azure function: {randomGuid}</p>
</main>
