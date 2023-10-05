<script lang="ts">
  import { onMount } from "svelte";
  import type Entry from "../types/Entry";
  import { error } from "@sveltejs/kit";

  let entries: Entry[] = [];
  let newMessage: string;
  let errorResult: string | undefined = undefined;

  async function getEntries() {
    const response = await fetch(`/api/dboEntry`);
    console.log(response);
    let jsonResult = await response.json();
    console.log(jsonResult);
    entries = jsonResult as Entry[];
    console.log(entries);
  }

  onMount(async () => {
    getEntries();
  });

  async function postEntry() {
    const res = await fetch(`/api/InsertEntry`, {
      method: "POST",
      body: JSON.stringify({ Message: newMessage }),
    }).catch((x) => {
      errorResult = x;
    });

    if (!errorResult && res) {
      const json = await res.json();
      console.log(json);
      getEntries();
    }
  }

  $: entries;
</script>

<main>
  <h1>Welcome to this website</h1>
  <p>Detta är ju en SWA, ingen SSR här inte...</p>

  <br />
  <h2>Inlägg</h2>
  <p>Skapa ett inlägg:</p>
  <input bind:value={newMessage} />
  <button on:click={postEntry}>Spara inlägg</button>

  {#if errorResult}
    <p style="color:red">Ajdå: {errorResult}</p>
  {/if}

  {#if entries}
    <p />
    <table>
      <tr>
        <th>Id</th>
        <th>Meddelande</th>
        <th>Skapat</th>
      </tr>
      {#each entries as entry}
        <tr>
          <td>{entry.Id}</td>
          <td>{entry.Message}</td>
          <td>{entry.CreateDate}</td>
        </tr>
      {/each}
    </table>
  {/if}
</main>

<style>
  table,
  th,
  td {
    border: 1px solid black;
  }
</style>
